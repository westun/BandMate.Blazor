using AutoMapper;
using BandMate.Domain.Core.Models;
using BandMate.MusicCatalog.Models;
using BandMate.Shared.DTO;
using BandMate.Shared.DTO.MusicCatalog;

namespace BandMate.Client.Configuration
{
    public class BandMateProfile : Profile
    {
        public BandMateProfile()
        {
            this.CreateMap<Account, AccountDTO>().ReverseMap();
            this.CreateMap<Band, BandDTO>().ReverseMap();
            this.CreateMap<BandName, BandNameDTO>().ReverseMap();
            this.CreateMap<Domain.Core.Models.Song, SongDTO>().ReverseMap();
            this.CreateMap<SongListType, SongListTypeDTO>().ReverseMap();

            this.CreateMap<Album, AlbumDTO>();
            this.CreateMap<Artist, ArtistDTO>();
            this.CreateMap<MusicCatalog.Models.Song, TrackDTO>();
        }
    }
}
