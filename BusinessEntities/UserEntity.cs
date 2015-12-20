using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class UserEntity
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public long OUmemberId { get; set; }
        public System.Guid UserGuid { get; set; }
        public OUmemberEntity OUmember { get; set; }
    }
}
