using AutoMapper;
using MovieHub.API.Constants;
using MovieHub.API.DTOs;
using MovieHub.API.Exceptions;
using MovieHub.API.Models;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class HallService : IHallService
    {
        IMapper _mapper;
        private readonly IHallRepository _hallRepository;

        public HallService(IMapper mapper, IHallRepository hallRepository)
        {
            _mapper = mapper;
            _hallRepository = hallRepository;
        }

        public async Task<HallReadDto> GetHallByIdAsync(int id)
        {
            return _mapper.Map<HallReadDto>(await GetHallByIdOrThrowAsync(id));
        }

        public async Task<HallReadDto> UpdateHallByIdAsync(
            int id,
            HallUpdateDto hallUpdateDto
        )
        {
            var hall = await GetHallByIdOrThrowAsync(id);
            _mapper.Map(hallUpdateDto, hall);
            await _hallRepository.SaveChangesAsync();
            return _mapper.Map<HallReadDto>(hall);
        }

        #region Private Methods
        private async Task<Hall> GetHallByIdOrThrowAsync(int id)
        {
            var hall = await _hallRepository.GetByIdAsync(id);
            if (hall is null)
            {
                throw new NotFoundException(
                    BusinessErrorMessages.Hall.NotFound
                );
            }
            return hall;
        }
        #endregion
    }
}
