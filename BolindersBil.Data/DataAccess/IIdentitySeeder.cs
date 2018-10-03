using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BolindersBil.Data.DataAccess
{
    public interface IIdentitySeeder
    {
        Task<bool> CreateAdminAccountIfEmpty();
    }
}
