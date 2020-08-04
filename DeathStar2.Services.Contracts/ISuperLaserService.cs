using DeathStar2.Model;

namespace DeathStar2.Services.Contracts
{
    /// <summary>
    /// A service for controlling the Death Star's super laser.
    /// </summary>
    public interface ISuperLaserService
    {
        /// <summary>
        /// Get the remaining capacity of the laser's energy store. 
        /// </summary>
        decimal GetCapacity();

        /// <summary>
        /// Set the capacity of the laser's energy store to a specific value.
        /// </summary>
        void SetCapacity(decimal capacity);

        /// <summary>
        /// Charge the laser. This will increment its capacity by a certain amount.
        /// </summary>
        void Charge();

        /// <summary>
        ///  Check Max charge. Not to exceed
        /// </summary>
        /// <returns></returns>
        bool CheckMaxCharge();

        /// <summary>
        /// Fire the laser at a target.
        /// </summary>
        /// <param name="target">The target to fire the laser at.</param>
        /// <returns>The power output of the laser.</returns>
        decimal Fire(Target target);

        /// <summary>
        /// Intial set up to save data
        /// </summary>
        void CreateSuperLaserTable();
    }
}
