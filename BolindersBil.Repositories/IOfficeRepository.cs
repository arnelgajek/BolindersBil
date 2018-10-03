using BolindersBil.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BolindersBil.Repositories
{
    public interface IOfficeRepository
    {
        IEnumerable<Office> Offices { get; }
        IEnumerable<Office> GetAllOffices();
    }
}
