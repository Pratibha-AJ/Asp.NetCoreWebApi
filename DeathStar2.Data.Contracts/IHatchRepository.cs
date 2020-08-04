using DeathStar2.Model;
using System.Collections.Generic;

namespace DeathStar2.Data.Contracts
{
    public interface IHatchRepository
    {
        IList<Hatch> GetHatches();
        Hatch GetHatch(int id);
    }
}
