using  DeathStar2.Model;
using System;
using System.ComponentModel.DataAnnotations;
using DeathStar2.Services;
using DeathStar2.Services.Contracts;
using DeathStar2.Data.Contracts;

namespace DeathStar2.Api.CustomValidationHelper
{
    public class MaxCapacity :  ValidationAttribute
    {
        private readonly ISuperLaserRepository _superLaserRepository;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (new SuperLaserService(_superLaserRepository).CheckMaxCharge())
                return new ValidationResult("Exceeds max capacity.");

            else return ValidationResult.Success;
           
        }
    }

    public class MinFireCapacity : ValidationAttribute
    {
        private readonly ISuperLaserRepository _superLaserRepository;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (new SuperLaserService(_superLaserRepository).GetCapacity() < 33.0M)
                return new ValidationResult("No enough power to fire.");

            else return ValidationResult.Success;

        }
    }
}
