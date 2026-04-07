using AutoMapper;
using BandMate.Blazor.DTO;
using BandMate.Blazor.Storage;
using BandMate.Domain.Core.Models;
using BandMate.Domain.Core.Repositores;

namespace BandMate.Blazor.Services.DAL
{
    public class SongService : ISongService
    {
        private readonly ISongRepository songRepository;
        private readonly IMapper mapper;
        private readonly IBandResourceManager bandResourceManager;

        public SongService(
            ISongRepository songRepository,
            IMapper mapper,
            IBandResourceManager bandResourceManager)
        {
            this.songRepository = songRepository;
            this.mapper = mapper;
            this.bandResourceManager = bandResourceManager;
        }

        public async Task<SongDTO> GetByIdAsync(int songID)
        {
            var song = await this.songRepository.GetAsync(songID);

            return this.mapper.Map<SongDTO>(song);
        }

        public async Task<IEnumerable<SongDTO>> GetSongsByBandIDAsync(int bandID)
        {
            var songs = await this.songRepository.GetByBandIDAsync(bandID);

            var songDTOs = this.mapper.Map<IEnumerable<SongDTO>>(songs);

            songDTOs = songDTOs.OrderBy(s => s.Artist).ThenBy(s => s.Title);

            songDTOs.Select(s =>
            {
                s.CreatedDateForDisplay = s.CreatedDate.ToString("d");
                return s;
            }).ToList();

            foreach (var song in songDTOs)
            {
                if (!string.IsNullOrWhiteSpace(song.TabFileName))
                {
                    song.TabFileName = await this.bandResourceManager
                        .GetSasUrlAsync(bandID, Path.GetFileName(song.TabFileName));
                }
                if (!string.IsNullOrWhiteSpace(song.LyricFileName))
                {
                    song.LyricFileName = await this.bandResourceManager
                        .GetSasUrlAsync(bandID, Path.GetFileName(song.LyricFileName));
                }
                if (!string.IsNullOrWhiteSpace(song.Mp3FileName))
                {
                    song.Mp3FileName = await this.bandResourceManager
                        .GetSasUrlAsync(bandID, Path.GetFileName(song.Mp3FileName));
                }
                if (!string.IsNullOrWhiteSpace(song.SheetMusicFileName))
                {
                    song.SheetMusicFileName = await this.bandResourceManager
                        .GetSasUrlAsync(bandID, Path.GetFileName(song.SheetMusicFileName));
                }
            }

            return songDTOs;
        }

        public async Task DeleteAsync(int id)
        {
            await this.songRepository.DeleteAsync(id);
        }

        public async Task<SongDTO> CreateAsync(SongDTO songDTO)
        {
            var song = this.mapper.Map<Song>(songDTO);

            var songFromDatabase = await this.songRepository.CreateAsync(song);

            return this.mapper.Map<SongDTO>(songFromDatabase);
        }

        public async Task<SongDTO> UpdateAsync(SongDTO songDTO)
        {
            var song = this.mapper.Map<Song>(songDTO);

            var songFromDatabase = await this.songRepository.UpdateAsync(song);

            return this.mapper.Map<SongDTO>(songFromDatabase);
        }
    }
}
