using DeathStar2.Data.Contracts;
using DeathStar2.Model;

namespace DeathStar2.Services
{
    public class TieFighterRepairService
    {
        private readonly ITieFighterRepository _repository;

        public TieFighterRepairService(ITieFighterRepository repository)
        {
            _repository = repository;
        }

        public void RepairTieFighters()
        {
            var tieFighters = _repository.GetDamagedTieFighters();
            //OutOfRangeException because of less than equal to condition
            //removed equal to for fixing issue

            for (var i = 0; i < tieFighters.Count; i++)
            {
                tieFighters[i].IsDamaged = false;
            }

            // We can achieve the same with foreach loop with less possiblities for such error
            foreach (var tf in tieFighters)
            {
                tf.IsDamaged = false;
            }
        }

        public void RepairTieFighterByCode(string code)
        {
            var fighter = _repository.GetTieFighterByCode(code);
            // Check if object is not null before accessing it
            if (fighter != null)
            {
                fighter.IsDamaged = false;
            }
        }

        public TieFighter CopyTieFighter(TieFighter tieFighter)
        {
            return new TieFighter()
            {
                Code = tieFighter.Code,
                IsDamaged = tieFighter.IsDamaged
                // IsDamaged = false   The value is hardcoded instead of copying
            };
        }
    }
}
