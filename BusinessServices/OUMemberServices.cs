using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

using AutoMapper;
using DataModel;
using DataModel.UnitOfWork;
using EntityDTO;
using BusinessServices.Contracts;

namespace BusinessServices
{
    public class OUMemberServices : IOUMemberServices
    {
        private readonly UnitOfWork _unitOfWork;

        public OUMemberServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public EntityDTO.OUmemberDTO GetOUMemberById(long ouMemberId)
        {
            var ouMember = _unitOfWork.OUMemberRepository.GetByID(ouMemberId);
            if (ouMember != null)
            {
                Mapper.CreateMap<OUmember, OUmemberDTO>();
                var ouMemberModel = Mapper.Map<OUmember, OUmemberDTO>(ouMember);
                return ouMemberModel;
            }
            return null;
        }

        public IEnumerable<EntityDTO.OUmemberDTO> GetOUMemberByName(string ouMemberName)
        {
            IEnumerable<DataModel.OUmember> ouMembers = _unitOfWork.OUMemberRepository.GetMany(x => (x.Firstname + " " + x.Lastname).StartsWith(ouMemberName));
            Mapper.CreateMap<OUmember, OUmemberDTO>();
            return Mapper.Map<IEnumerable<OUmember>, IEnumerable<OUmemberDTO>>(ouMembers);
        }

        public IEnumerable<EntityDTO.OUmemberDTO> GetAllOUMembers()
        {
            var ouMembers = _unitOfWork.OUMemberRepository.GetAll().ToList();
            if(ouMembers.Any())
            {
                Mapper.CreateMap<OUmember, OUmemberDTO>();
                var ouMembersModel = Mapper.Map<List<OUmember>, List<OUmemberDTO>>(ouMembers);
                return ouMembersModel;
            }
            return null;
        }

        public long CreateOUMember(EntityDTO.OUmemberDTO ouMemberEntity)
        {
            using(var scope = new TransactionScope())
            {
                var ouMember = new OUmember
                {
                    Firstname = ouMemberEntity.Firstname,
                    Lastname = ouMemberEntity.Lastname,
                    Age = ouMemberEntity.Age
                };

                _unitOfWork.OUMemberRepository.Insert(ouMember);
                _unitOfWork.Save();
                scope.Complete();
                return ouMember.Id;
            }
        }

        public bool UpdateOUMember(long ouMemberId, EntityDTO.OUmemberDTO ouMemberEntity)
        {
            var success = false;
            if(ouMemberEntity != null)
            {
                using(var scope = new TransactionScope())
                {
                    var ouMember = _unitOfWork.OUMemberRepository.GetByID(ouMemberId);
                    if (ouMember != null)
                    {
                        ouMember.Firstname = ouMemberEntity.Firstname;
                        ouMember.Lastname = ouMemberEntity.Lastname;
                        ouMember.Age = ouMemberEntity.Age;
                        _unitOfWork.OUMemberRepository.Update(ouMember);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DeleteOUMember(long ouMemberId)
        {
            var success = false;
            if(ouMemberId > 0)
            {
                using(var scope = new TransactionScope())
                {
                    var ouMember = _unitOfWork.OUMemberRepository.GetByID(ouMemberId);
                    if (ouMember != null)
                    {
                        _unitOfWork.OUMemberRepository.Delete(ouMember);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

    }
}
