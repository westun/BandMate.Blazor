using BandMate.MusicCatalog.Models;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using System.Net.Http.Headers;
using System.Text;

namespace BandMate.MusicCatalog.Persistence
{
    public class SpotifyArtistRepository : IArtistRepository
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly Uri BaseAddress = new Uri("https://api.spotify.com/v1");
        private readonly SpotifySettings spotifySettings;

        public SpotifyArtistRepository(SpotifySettings spotifySettings)
        {
            this.spotifySettings = spotifySettings ?? throw new ArgumentNullException(nameof(spotifySettings));
        }

        public async Task<IEnumerable<Artist>> SearchArtistsAsync(string name)
        {
            var token = await GetAccessToken();

            var spotifyClient = new SpotifyClient(token.Access_Token);

            var searchRequest = new SearchRequest(SearchRequest.Types.Artist, name);
            var searchResult = await spotifyClient.Search.Item(searchRequest);

            var artists = searchResult.Artists.Items.Select(i => new Artist
            {
                Id = i.Id,
                Name = i.Name,
                ImageUrl = i.Images.LastOrDefault()?.Url, //getting smallest image
            });

            return artists;
        }

        public async Task<IEnumerable<Album>> GetAlbums(string artistId)
        {
            if (string.IsNullOrEmpty(artistId))
            {
                throw new System.Exception("id cannot be empty or null");
            }

            var albumsWithoutTracks = await this.GetAlbumsWithoutTracks(artistId);

            //only get albums in US markets to remove duplicate albums
            albumsWithoutTracks = albumsWithoutTracks.Where(a => a.Available_Markets.Contains("US"));

            var albumIds = albumsWithoutTracks.Select(d => d.Id);
            var albumsWithTracks = await this.GetAlbumsWithTracks(albumIds.ToArray());

            return albumsWithTracks.Select(a => new Album
            {
                Id = a.Id,
                Name = a.Name,
                AlbumImgCoverUrl = a.Images.LastOrDefault()?.Url, //getting smallest image
                Release_Date = a.Release_Date,
                Tracks = a.Tracks.Items.Select(t => new Song
                { 
                   Id = t.Id,
                   Name = t.Name,
                   Track_Number = t.Track_Number,
                }),
            });
        }

        private async Task<IEnumerable<AlbumDTO>> GetAlbumsWithoutTracks(string artistId)
        {
            var token = await GetAccessToken();
            var limit = 50;
            var getAllArtistAlbumsEndpoint = new Uri($"{BaseAddress}/artists/{artistId}/albums?limit={limit}");
            var albumsWithoutTracks = await this.MakeWebRequest<AlbumItemDTO>(getAllArtistAlbumsEndpoint, HttpMethod.Get, token.Access_Token);

            if (albumsWithoutTracks.Next == null)
            {
                return albumsWithoutTracks.Items;
            }

            string nextUrl = albumsWithoutTracks.Next;
            do
            {
                var nextUri = new Uri(nextUrl);
                if (nextUri.Host != "api.spotify.com")
                {
                    throw new Exception("nextUrl host is invalid.");
                }

                var nextAlbumsWithoutTracks = await this.MakeWebRequest<AlbumItemDTO>(nextUri, HttpMethod.Get, token.Access_Token);
                albumsWithoutTracks.Items = albumsWithoutTracks.Items.Concat(nextAlbumsWithoutTracks.Items).ToArray();

                nextUrl = nextAlbumsWithoutTracks.Next;

            } while (nextUrl != null);

            return albumsWithoutTracks.Items;
        }

        private async Task<IEnumerable<AlbumDTO>> GetAlbumsWithTracks(params string[] albumIds)
        {
            var token = await GetAccessToken();

            var chunks = this.GetAlbumIdsInChunks(chunkSize: 20, albumIds: albumIds);

            var getSeveralAlbumsTasks = chunks.Select(albumIdsChunk => this.MakeWebRequest<AlbumCollectionDTO>(new Uri($"{BaseAddress}/albums?ids={string.Join(",", albumIdsChunk)}"), HttpMethod.Get, token.Access_Token));
            var getSeveralAlbumsResults = await Task.WhenAll(getSeveralAlbumsTasks);

            return getSeveralAlbumsResults.SelectMany(r => r.Albums);
        }

        private List<string[]> GetAlbumIdsInChunks(int chunkSize, params string[] albumIds)
        {
            var chunks = new List<string[]>();
            for (int i = 0; i * chunkSize < albumIds.Length; i++)
            {
                var skipCount = i * chunkSize;
                var takeCount = 20;
                var chunk = albumIds.Skip(skipCount).Take(takeCount).ToArray();
                if (!chunk.Any())
                {
                    break;
                }

                chunks.Add(chunk);
            }

            return chunks;
        }

        private async Task<T> MakeWebRequest<T>(Uri requestUri, HttpMethod httpMethod, string authToken)
        { 
            var request = new HttpRequestMessage();
            request.Method = httpMethod;
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            request.RequestUri = requestUri;

            var response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("request was unsuccessful");
            }

            return JsonConvert.DeserializeObject<T>(responseString);
        }

        private async Task<AccessTokenDTO> GetAccessToken()
        {
            var endpoint = new Uri("https://accounts.spotify.com/api/token");

            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Headers.Authorization = new AuthenticationHeaderValue("basic", this.Base64Encode($"{this.spotifySettings.ClientId}:{this.spotifySettings.ClientSecret}"));
            request.RequestUri = endpoint;
            request.Content = new StringContent("grant_type=client_credentials");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Http status code is not successful when getting access token");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<AccessTokenDTO>(responseString);

            return token;
        }

        private string Base64Encode(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        private class AccessTokenDTO
        {
            public string Access_Token { get; set; }
            public string Token_Type { get; set; }
            public string Expires_In { get; set; }
        }

        private class AlbumItemDTO
        {
            public string Next { get; set; }
            public AlbumDTO[] Items { get; set; }
        }

        private class AlbumCollectionDTO
        {
            public AlbumDTO[] Albums { get; set; }
        }

        private class AlbumDTO
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Release_Date { get; set; }
            public string[] Available_Markets { get; set; }
            public AlbumImage[] Images { get; set; }
            public SongItemDTO Tracks { get; set; }
        }

        private class SongItemDTO
        {
            public SongDTO[] Items { get; set; }
        }

        private class SongDTO
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Track_Number { get; set; }
        }

        private class AlbumImage
        {
            public string Url { get; set; }
            public string Width { get; set; }
            public string Height { get; set; }
        }
    }
}