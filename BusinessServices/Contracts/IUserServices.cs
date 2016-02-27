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
        EntityDTO.UserResponseDTO CreateUserLogin(string username, string saltedHashedPassword);
        EntityDTO.UserDTO GetUserLogin(string username);
        bool CheckUserToken(string userToken);
    }
}
