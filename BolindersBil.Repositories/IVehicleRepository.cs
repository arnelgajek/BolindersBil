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

        void AddNewVehicle(Vehicle vehicle);
        IEnumerable<Vehicle> GetAllVehicles();
<<<<<<< HEAD

        Vehicle DeleteVehicle(int vehicleId);

=======
        void UpdateVehicle(EditVehicleViewModel v);
>>>>>>> 2b3542891beeed45f99092c63fba2d4d6bb6fb43
    }
}
