using AutoMapper;
using BandMate.Shared.DTO;
using BandMate.Shared.Services.DAL;
using BandMate.Shared.Storage;
using System.Net.Http.Json;

namespace BandMate.Shared.Services.Api
{
    public class SongApiService : ISongService
    {
        private readonly IMapper mapper;
        private readonly IBandResourceManager bandResourceManager;
        private readonly HttpClient httpClient;

        public SongApiService(
            IMapper mapper,
            IBandResourceManager bandResourceManager,
            HttpClient httpClient)
        {
            this.mapper = mapper;
            this.bandResourceManager = bandResourceManager;
            this.httpClient = httpClient;
        }

        public async Task<SongDTO> GetByIdAsync(int songID)
        {
            throw new NotImplementedException();

            //var song = await this.songRepository.GetAsync(songID);

            //return this.mapper.Map<SongDTO>(song);
        }

        public async Task<IEnumerable<SongDTO>> GetSongsByBandIDAsync(int bandID)
        {
            var songDTOs = await httpClient.GetFromJsonAsync<IEnumerable<SongDTO>>($"api/song?bandID={bandID}");

            songDTOs.OrderBy(s => s.Artist)
                .ThenBy(s => s.Title)
                .Select(s =>
            {
                s.CreatedDateForDisplay = s.CreatedDate.ToString("d");
                return s;
            }).ToList();

            foreach (var song in songDTOs)
            {
                await this.SetSasUrls(song, bandID);
            }

            return songDTOs;
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
            //await this.songRepository.DeleteAsync(id);
        }

        public async Task<SongDTO> CreateAsync(SongDTO songDTO)
        {
            throw new NotImplementedException();

            //var song = this.mapper.Map<Song>(songDTO);

            //var songFromDatabase = await this.songRepository.CreateAsync(song);

            //return this.mapper.Map<SongDTO>(songFromDatabase);
        }

        public async Task<SongDTO> UpdateAsync(SongDTO songDTO)
        {
            throw new NotImplementedException();

            //await this.songRepository.DeleteAsync(id);

            //var song = this.mapper.Map<Song>(songDTO);

            //var songFromDatabase = await this.songRepository.UpdateAsync(song);

            //return this.mapper.Map<SongDTO>(songFromDatabase);
        }

        private async Task SetSasUrls(SongDTO song, int bandID)
        {
            if (!string.IsNullOrWhiteSpace(song.TabFileName))
            {
                song.TabFileNameSasUrl = await this.bandResourceManager
                    .GetSasUrlAsync(bandID, Path.GetFileName(song.TabFileName));
            }
            if (!string.IsNullOrWhiteSpace(song.LyricFileName))
            {
                song.LyricFileNameSasUrl = await this.bandResourceManager
                    .GetSasUrlAsync(bandID, Path.GetFileName(song.LyricFileName));
            }
            if (!string.IsNullOrWhiteSpace(song.Mp3FileName))
            {
                song.Mp3FileNameSasUrl = await this.bandResourceManager
                    .GetSasUrlAsync(bandID, Path.GetFileName(song.Mp3FileName));
            }
            if (!string.IsNullOrWhiteSpace(song.SheetMusicFileName))
            {
                song.SheetMusicFileNameSasUrl = await this.bandResourceManager
                    .GetSasUrlAsync(bandID, Path.GetFileName(song.SheetMusicFileName));
            }
        }
    }
}
