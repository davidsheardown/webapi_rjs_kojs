using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntityDTO;

namespace BusinessServices.Contracts
{
    public interface IUserServices
    {
        EntityDTO.LoginResponseDTO GetUserLogin(string username, string password);
        bool CheckUserToken(string userToken);
    }
}
