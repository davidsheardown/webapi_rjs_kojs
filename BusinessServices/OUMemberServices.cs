using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

using AutoMapper;
using DataModel;
using DataModel.UnitOfWork;
using BusinessEntities;
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

        public BusinessEntities.OUmemberEntity GetOUMemberById(long ouMemberId)
        {
            var ouMember = _unitOfWork.OUMemberRepository.GetByID(ouMemberId);
            if (ouMember != null)
            {
                Mapper.CreateMap<OUmember, OUmemberEntity>();
                var ouMemberModel = Mapper.Map<OUmember, OUmemberEntity>(ouMember);
                return ouMemberModel;
            }
            return null;
        }

        public IEnumerable<BusinessEntities.OUmemberEntity> GetOUMemberByName(string ouMemberName)
        {
            IEnumerable<DataModel.OUmember> ouMembers = _unitOfWork.OUMemberRepository.GetMany(x => (x.Firstname + " " + x.Lastname).StartsWith(ouMemberName));
            Mapper.CreateMap<OUmember, OUmemberEntity>();
            return Mapper.Map<IEnumerable<OUmember>, IEnumerable<OUmemberEntity>>(ouMembers);
        }

        public IEnumerable<BusinessEntities.OUmemberEntity> GetAllOUMembers()
        {
            var ouMembers = _unitOfWork.OUMemberRepository.GetAll().ToList();
            if(ouMembers.Any())
            {
                Mapper.CreateMap<OUmember, OUmemberEntity>();
                var ouMembersModel = Mapper.Map<List<OUmember>, List<OUmemberEntity>>(ouMembers);
                return ouMembersModel;
            }
            return null;
        }

        public long CreateOUMember(BusinessEntities.OUmemberEntity ouMemberEntity)
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

        public bool UpdateOUMember(long ouMemberId, BusinessEntities.OUmemberEntity ouMemberEntity)
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
