using System;
using System.Collections.Generic;
using System.Text;
using DeathStar2.Model;

namespace DeathStar2.Data.Contracts
{
    public interface ISuperLaserRepository
    { 
        void CreateSuperLaserTable();

        decimal GetCapacity();

        void ChargePower();

        void SetCapacity(decimal capacity);

        decimal Fire(Target target);
    }
}
