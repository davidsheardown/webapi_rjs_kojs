using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntityDTO;

namespace BusinessServices.Contracts
{
    public interface IOUMemberServices
    {
        OUmemberDTO GetOUMemberById(long OUMemberId);
        IEnumerable<EntityDTO.OUmemberDTO> GetOUMemberByName(string ouMemberName);
        IEnumerable<OUmemberDTO> GetAllOUMembers();
        long CreateOUMember(OUmemberDTO ouMemberEntity);
        bool UpdateOUMember(long OUMemberId, OUmemberDTO ouMemberEntity);
        bool DeleteOUMember(long OUMemberId);
    }
}
