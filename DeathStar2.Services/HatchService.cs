using DeathStar2.Data.Contracts;
using DeathStar2.Model;

namespace DeathStar2.Services
{
    public class HatchService
    {
        private readonly IHatchRepository _hatchRepository;

        public HatchService(IHatchRepository hatchRepository)
        {
            _hatchRepository = hatchRepository;
        }

        public Hatch OpenHatch(int id)
        {
            var hatch = _hatchRepository.GetHatch(id);
            hatch.IsOpen = true;

            return hatch;
        }

        public Hatch CloseHatch(int id)
        {
            var hatch = _hatchRepository.GetHatch(id);
            hatch.IsOpen = false;

            return hatch;
        }
    }
}
