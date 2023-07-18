using CarApp.Entities;
using CarApp.Models;
using NuGet.Configuration;

namespace CarApp.Interfaces
{
    public interface ICarView
    {
        Task<List<CarViewModel>> GetAll();
        Task<List<CarViewModel>> FilterCars(int fuelId, int transmissionId);
        Task<List<Car>> FilterCars2(IEnumerable<int> vehicleTypes, IEnumerable<int> carBodyTypes,
            IEnumerable<int> Brands, IEnumerable<int> fuelTypes, IEnumerable<int> driveTypes,
            IEnumerable<int> transmissionTypes);
        Task<Car> GetById(int id);
        Task Update(CarViewModel carViewModel);
        Task AddNew(CarViewModel carViewModel);
        Task Delete(CarViewModel car);
        Task<List<Fuel>> GetFuel();
        Task<List<Drive>> GetDrive();
        Task<List<CarBodyType>> GetCarBody();
        Task<List<VehicleType>> GetVehicleType();
        Task<List<Brand>> GetBrand();
        Task<List<Transmission>> GetTransmission();
    }
}
