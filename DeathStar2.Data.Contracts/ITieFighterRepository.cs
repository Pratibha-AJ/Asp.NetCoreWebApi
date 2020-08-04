using DeathStar2.Model;
using System.Collections.Generic;

namespace DeathStar2.Data.Contracts
{
    public interface ITieFighterRepository
    {
        IList<TieFighter> GetDamagedTieFighters();
        TieFighter GetTieFighterByCode(string code);
    }
}
