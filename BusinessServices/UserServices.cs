using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using DataModel;
using DataModel.UnitOfWork;
using BusinessEntities;
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

        public BusinessEntities.LoginResponseEntity GetUserLogin(string username, string password)
        {
            var user = _unitOfWork.UserRepository.Get(x => x.Username == username && x.Password == password);
            if (user != null)
            {
                Mapper.CreateMap<User, LoginResponseEntity>();
                return Mapper.Map<User, LoginResponseEntity>(user);
            }
            return null;
        }
    }
}
