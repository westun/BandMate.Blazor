using AutoMapper;
using BandMate.Blazor.DTO;
using BandMate.Domain.Core.Models;

namespace BandMate.Blazor.Configuration
{
    public class BandMateProfile : Profile
    {
        public BandMateProfile()
        {
            this.CreateMap<Song, SongDTO>().ReverseMap();
            this.CreateMap<SongListType, SongListTypeDTO>().ReverseMap();
        }
    }
}
