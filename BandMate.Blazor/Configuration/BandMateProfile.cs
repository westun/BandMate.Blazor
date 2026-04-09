using AutoMapper;
using BandMate.Domain.Core.Models;
using BandMate.MusicCatalog.Models;
using BandMate.Shared.DTO;
using BandMate.Shared.DTO.MusicCatalog;
using BandMate.Shared.ViewModels;

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

            //don't overwrite properties in the destination object if the source doesn't have values for them
            //handle both null for objects and default values for primitives
            CreateMap<SongDTO, EditSongModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !srcMember.Equals(GetDefault(srcMember.GetType()))));

            CreateMap<EditSongModel, SongDTO>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !srcMember.Equals(GetDefault(srcMember.GetType()))));

            this.CreateMap<Album, AlbumDTO>();
            this.CreateMap<Artist, ArtistDTO>();
            this.CreateMap<MusicCatalog.Models.Song, TrackDTO>();
        }
        
        // Helper method to get the default value for any type
        private static object GetDefault(Type type) => type.IsValueType ? Activator.CreateInstance(type) : null;
    }
}
