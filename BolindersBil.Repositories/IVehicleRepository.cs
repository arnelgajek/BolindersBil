using BolindersBil.Models;
using System;
using System.Collections.Generic;
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

        Vehicle Vehicle(int vehicleId);

        IEnumerable<Vehicle> Search(string searchString, bool Used);

        void UpdateVehicle(EditVehicleViewModel v);
    }
}
