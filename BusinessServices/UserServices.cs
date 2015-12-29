using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using DataModel;
using DataModel.UnitOfWork;
using EntityDTO;
using BusinessServices.Contracts;

namespace BusinessServices
{
    public class UserServices : IUserServices
    {
        private readonly UnitOfWork _unitOfWork;

        public UserServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public EntityDTO.LoginResponseDTO GetUserLogin(string username, string password)
        {
            var user = _unitOfWork.UserRepository.Get(x => x.Username == username && x.Password == password);
            if (user != null)
            {
                Mapper.CreateMap<User, LoginResponseDTO>();
                return Mapper.Map<User, LoginResponseDTO>(user);
            }
            return null;
        }

        public bool CheckUserToken(string userToken)
        {
            var tokenGuid = Guid.Parse(userToken);
            var tokenResponse = _unitOfWork.UserRepository.Get(x => x.UserGuid == tokenGuid && x.UserActive == true);
            if (tokenResponse != null)
            {
                return true;
            }
            return false;
        }
    }
}
