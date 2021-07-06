using AutoMapper;

namespace Otium.OmdbProvider
{
    public class OmdbMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "OtiumOmdbProviderOmdbMappingProfile"; }
        }

        public OmdbMappingProfile()
        {
            CreateMap<Entities.Movie, MovieComponent.MovieModel>()
                .ForMember(x => x.OriginalTitle, opt => opt.MapFrom(x => x.Title))
                .ForMember(x => x.ReleaseYear, opt => opt.MapFrom(x => x.Year))
                .ForMember(x => x.ImdbId, opt => opt.MapFrom(x => x.ImdbID))
                .ForMember(x => x.ImdbRating, opt => opt.MapFrom(x => x.ImdbRating))
                .ForMember(x => x.PosterUrl, opt => opt.MapFrom(x => x.Poster));

            CreateMap<Entities.Search, MovieComponent.MovieModel>()
                .ForMember(x => x.OriginalTitle, opt => opt.MapFrom(x => x.Title))
                .ForMember(x => x.ReleaseYear, opt => opt.MapFrom(x => x.Year))
                .ForMember(x => x.ImdbId, opt => opt.MapFrom(x => x.ImdbID))
                .ForMember(x => x.ImdbRating, opt => opt.Ignore())
                .ForMember(x => x.PosterUrl, opt => opt.MapFrom(x => x.Poster));
        }
    }
}
