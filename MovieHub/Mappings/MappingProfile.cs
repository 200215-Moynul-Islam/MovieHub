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
            #endregion
        }
    }
}
