using AutoMapper;
using MovieHub.API.DTOs;
using MovieHub.API.Models;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class BranchHallSeatService : IBranchHallSeatService
    {
        private readonly IMapper _mapper;
        private readonly IBranchRepository _branchRepository;
        private readonly IHallRepository _hallRepository;
        private readonly ISeatRepository _seatRepository;

        public BranchHallSeatService(
            IMapper mapper,
            IBranchRepository branchRepository,
            IHallRepository hallRepository,
            ISeatRepository seatRepository
        )
        {
            _mapper = mapper;
            _branchRepository = branchRepository;
            _hallRepository = hallRepository;
            _seatRepository = seatRepository;
        }

        public async Task<int> CreateHallWithSeatsAsync(
            HallCreateDto hallCreateDto
        )
        {
            await EnsureBranchExistsByIdOrThrowAsync(hallCreateDto.BranchId);
            await EnsureHallNameIsUniqueForBranchOrThrowAsync(
                hallCreateDto.Name,
                hallCreateDto.BranchId
            );

            // Prepare the hall.
            var hall = _mapper.Map<Hall>(hallCreateDto);

            // Prepare all the seats.
            var seats = new List<Seat>();
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
                    seats.Add(
                        new Seat()
                        {
                            Hall = hall,
                            SeatNumber =
                                $"{GetRowLabelFromIntOrThrow(rowNumber)}{colNumber}",
                        }
                    );
                }
            }

            await _hallRepository.ExecuteInTransactionAsync(async () =>
            {
                await _hallRepository.CreateAsync(hall);
                await _seatRepository.CreateAsync(seats);
                await _seatRepository.SaveChangesAsync();
            });
            return hall.Id;
        }

        #region Private Methods
        private async Task EnsureBranchExistsByIdOrThrowAsync(int id)
        {
            bool exists = await _branchRepository.ExistsByIdAsync(id);
            if (!exists)
            {
                throw new Exception($"Branch with id '{id}' does not exist.");
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
                throw new Exception($"Hall with name '{name}' already exists."); // Use specific exception and avoid magic exception message.
            }
        }

        private string GetRowLabelFromIntOrThrow(int rowNumber)
        {
            if (rowNumber < 1)
            {
                throw new Exception(
                    "Number must be greater greater than 0 (zero)."
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
