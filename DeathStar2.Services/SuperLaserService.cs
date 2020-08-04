using System;
using System.Collections.Generic;
using System.Text;
using DeathStar2.Services.Contracts;
using DeathStar2.Model;
using DeathStar2.Data.Contracts;

namespace DeathStar2.Services
{
    public class SuperLaserService : ISuperLaserService
    {
        private const decimal MaxCharge = 100.0M;
        private const decimal MinCharge = 0.0M;
        private const decimal MinFireCharge = 33.0M;
        private const decimal UnitCharge = 10.0M;
        private readonly ISuperLaserRepository _superLaserRepository;

        public SuperLaserService(ISuperLaserRepository superLaserRepository)
        {
            _superLaserRepository = superLaserRepository;
        }

        public void CreateSuperLaserTable()
        {
            _superLaserRepository.CreateSuperLaserTable();
        }

        public decimal GetCapacity()
        {    
            return _superLaserRepository.GetCapacity();
        }

        void ISuperLaserService.SetCapacity(decimal capacity)
        {
            if (capacity <=MaxCharge  && capacity >= MinCharge)
            {
                _superLaserRepository.SetCapacity(capacity);
            }            
        }
        
        void ISuperLaserService.Charge()
        {
            if (CheckMaxCharge())
            {
                _superLaserRepository.ChargePower();
            }
        }

        public decimal Fire(Target target)
        {
            decimal poweroutput = 0.0M;

            if (GetCapacity() > MinFireCharge)
            {
                poweroutput = _superLaserRepository.Fire(target);
            }
            else
            {
                throw new NotEnoughChargeException();
            }           

            return poweroutput;
        }

        public bool CheckMaxCharge()
        {
            var newcapacity = _superLaserRepository.GetCapacity() + UnitCharge;
            if (newcapacity > MaxCharge)
            {
                return false;

            }
            else
               return true;

        }

        
    }
}
