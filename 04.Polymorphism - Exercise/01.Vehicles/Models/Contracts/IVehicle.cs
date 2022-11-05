
namespace Vehicles.Models.Contracts
{
    public interface IVehicle
    {
        string Drive(double distance);

        void Refuel(double fuelAmount);
    }
}
