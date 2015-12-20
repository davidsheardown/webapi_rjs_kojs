using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessEntities;

namespace BusinessServices.Contracts
{
    public interface IUserServices
    {
        BusinessEntities.LoginResponseEntity GetUserLogin(string username, string password);
    }
}
