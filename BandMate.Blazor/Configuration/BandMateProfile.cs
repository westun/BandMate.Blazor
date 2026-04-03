using AutoMapper;
using BandMate.Blazor.DTO;
using BandMate.Blazor.DTO.MusicCatalog;
using BandMate.Domain.Core.Models;
using BandMate.MusicCatalog.Models;

namespace BandMate.Blazor.Configuration
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
