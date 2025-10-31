using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class HallService : IHallService
    {
        private readonly IHallRepository _hallRepository;

        public HallService(IHallRepository hallRepository)
        {
            _hallRepository = hallRepository;
        }

        public async Task DeactivateHallsByBranchIdAsync(int branchId)
        {
            await _hallRepository.DeactivateHallsByBranchIdAsync(branchId);
        }
    }
}
