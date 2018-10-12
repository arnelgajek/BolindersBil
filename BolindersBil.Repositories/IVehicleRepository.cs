using BolindersBil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BolindersBil.Repositories
{
    public interface IVehicleRepository
    {
        // The classes that implement this interface will return an IEnumerable<Vehicle>.
        // To be able to get a list of all Vehicles from the DB.
        IEnumerable<Vehicle> Vehicles { get; }
        IEnumerable<Image> Images { get; }        

        void AddImage(Image image);

        void AddNewVehicle(Vehicle vehicle);

        IEnumerable<Vehicle> GetAllVehicles();

        IEnumerable<Image> GetAllImages();

        Vehicle DeleteVehicle(int vehicleId);

        Vehicle BulkDeleteVehicle(int vehicleId);
        
        IEnumerable<Vehicle> Search(string searchString, bool? Used);

        Vehicle Vehicle(int vehicleId);

        IEnumerable<Vehicle> FilterSearch(string year, string fuel, string body, string gearbox, double minPrice, double maxPrice, int maxKm);

        IEnumerable<Vehicle> GetNewVehicles();

        IEnumerable<Vehicle> GetUsedVehicles();
      
        void UpdateVehicle(EditVehicleViewModel v);

    }
}
