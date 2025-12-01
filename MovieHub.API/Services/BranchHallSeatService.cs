using AutoMapper;
using MovieHub.API.Constants;
using MovieHub.API.DTOs;
using MovieHub.API.Exceptions;
using MovieHub.API.Models;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class BranchHallSeatService : IBranchHallSeatService
    {
        private readonly IMapper _mapper;
        private readonly IBranchRepository _branchRepository;
        private readonly IHallRepository _hallRepository;

        public BranchHallSeatService(
            IMapper mapper,
            IBranchRepository branchRepository,
            IHallRepository hallRepository
        )
        {
            _mapper = mapper;
            _branchRepository = branchRepository;
            _hallRepository = hallRepository;
        }

        public async Task<HallReadDto> CreateHallWithSeatsAsync(
            HallCreateDto hallCreateDto
        )
        {
            await EnsureBranchExistsByIdOrThrowAsync(
                hallCreateDto.BranchId!.Value
            );
            await EnsureHallNameIsUniqueForBranchOrThrowAsync(
                hallCreateDto.Name!,
                hallCreateDto.BranchId!.Value
            );

            // Prepare the hall.
            var hall = _mapper.Map<Hall>(hallCreateDto);

            // Prepare all the seats inside the hall.
            hall.Seats = new List<Seat>();
            for (
                int rowNumber = 1;
                rowNumber <= hallCreateDto.TotalRows;
                rowNumber++
            )
            {
                for (
                    int colNumber = 1;
                    colNumber <= hallCreateDto.TotalColumns;
                    colNumber++
                )
                {
                    hall.Seats.Add(
                        new Seat()
                        {
                            Hall = hall,
                            SeatNumber =
                                $"{GetRowLabelFromIntOrThrow(rowNumber)}{colNumber}",
                        }
                    );
                }
            }

            await _hallRepository.CreateAsync(hall);
            await _hallRepository.SaveChangesAsync();
            return _mapper.Map<HallReadDto>(hall);
        }

        #region Private Methods
        private async Task EnsureBranchExistsByIdOrThrowAsync(int id)
        {
            bool exists = await _branchRepository.ExistsByIdAsync(id);
            if (!exists)
            {
                throw new NotFoundException(
                    BusinessErrorMessages.Branch.NotFound
                );
            }
        }

        private async Task EnsureHallNameIsUniqueForBranchOrThrowAsync(
            string name,
            int branchId
        )
        {
            if (
                await _hallRepository.HallNameExistsForBranchCaseInsensitiveAsync(
                    name,
                    branchId
                )
            )
            {
                throw new ConflictException(
                    BusinessErrorMessages.Hall.NameUnavailable
                );
            }
        }

        private string GetRowLabelFromIntOrThrow(int rowNumber)
        {
            if (rowNumber < 1)
            {
                throw new ArgumentOutOfRangeException(
                    null,
                    BusinessErrorMessages.Seat.RowNumberOutOfRange
                );
            }

            string result = "";
            while (rowNumber > 0)
            {
                rowNumber--;
                int remainder = rowNumber % 26;
                rowNumber /= 26;
                char letter = (char)('A' + remainder);
                result = letter + result;
            }
            return result;
        }
        #endregion
    }
}
