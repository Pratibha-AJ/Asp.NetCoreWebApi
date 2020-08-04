using System;

namespace DeathStar2.Services.Contracts
{
    public class NotEnoughChargeException : Exception
    {
        public NotEnoughChargeException()
        {

        }

        public NotEnoughChargeException(string message) : base(message)
        {
        }

        public NotEnoughChargeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
