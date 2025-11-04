using AutoMapper;
using MovieHub.API.DTOs;

namespace MovieHub.API.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMapper _mapper;

        public MovieService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<int> CreateMovieAsync(
            MovieCreateDto movieCreateDto
        ) { }
    }
}
