using AutoMapper;
using MovieHub.API.DTOs;
using MovieHub.API.Models;

namespace MovieHub.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Branch Mappings
            CreateMap<BranchCreateDto, Branch>();
            CreateMap<Branch, BranchReadDto>();
            CreateMap<BranchUpdateDto, Branch>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember is not null)
                );
            #endregion
        }
    }
}
