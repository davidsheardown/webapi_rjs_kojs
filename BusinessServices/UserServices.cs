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
using System.Transactions;

namespace BusinessServices
{
    public class UserServices : IUserServices
    {
        private readonly UnitOfWork _unitOfWork;

        public UserServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public EntityDTO.UserResponseDTO CreateUserLogin(string username, string saltedHashedPassword)
        {
            using (var scope = new TransactionScope())
            {
                var user = new User
                {
                    Username = username,
                    Password = saltedHashedPassword,
                    LastUpdated = DateTime.UtcNow,
                    DateCreated = DateTime.UtcNow,
                    UserGuid = Guid.NewGuid(),
                    UserActive = true
                };
                _unitOfWork.UserRepository.Insert(user);
                _unitOfWork.Save();
                scope.Complete();
                Mapper.CreateMap<User, UserResponseDTO>();
                return Mapper.Map<User, UserResponseDTO>(user);
            }
        }

        public EntityDTO.UserDTO GetUserLogin(string username)
        {
            var user = _unitOfWork.UserRepository.Get(x => x.Username == username);
            if (user != null)
            {
                Mapper.CreateMap<User, UserDTO>();
                return Mapper.Map<User, UserDTO>(user);
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
