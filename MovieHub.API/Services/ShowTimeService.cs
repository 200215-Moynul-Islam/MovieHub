using AutoMapper;
using MovieHub.API.DTOs;
using MovieHub.API.Models;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class ShowTimeService : IShowTimeService
    {
        private readonly IMapper _mapper;
        private readonly IShowTimeRepository _showTimeRepository;

        public ShowTimeService(
            IMapper mapper,
            IShowTimeRepository showTimeRepository
        )
        {
            _mapper = mapper;
            _showTimeRepository = showTimeRepository;
        }

        public async Task<ShowTimeDetailsReadDto> GetShowTimeDetailsByIdAsync(
            int id
        )
        {
            var showtime = await GetShowTimeDetailsByIdOrThrowAsync(id);
            return _mapper.Map<ShowTimeDetailsReadDto>(showtime);
        }

        #region Private Methods
        private async Task<ShowTime> GetShowTimeDetailsByIdOrThrowAsync(int id)
        {
            var showtime =
                await _showTimeRepository.GetShowTimeDetailsByIdAsync(id);
            if (showtime is null)
            {
                throw new Exception($"ShowTime with id '{id}' does not exist.");
            }
            return showtime;
        }
        #endregion
    }
}
