using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessEntities;

namespace BusinessServices.Contracts
{
    public interface IOUMemberServices
    {
        OUmemberEntity GetOUMemberById(long OUMemberId);
        IEnumerable<BusinessEntities.OUmemberEntity> GetOUMemberByName(string ouMemberName);
        IEnumerable<OUmemberEntity> GetAllOUMembers();
        long CreateOUMember(OUmemberEntity ouMemberEntity);
        bool UpdateOUMember(long OUMemberId, OUmemberEntity ouMemberEntity);
        bool DeleteOUMember(long OUMemberId);
    }
}
