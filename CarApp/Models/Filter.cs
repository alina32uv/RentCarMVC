using CarApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarApp.Models
{
    [Keyless]
    public class Filter
    {
        public List<Car> Cars{get; set;}
        public List<CarBodyType> CarBodyTypes { get; set; }
        public List<VehicleType> VehicleTypes { get; set; }

    }
}
