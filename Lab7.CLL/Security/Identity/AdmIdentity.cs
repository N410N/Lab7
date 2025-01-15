using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.CCL.Security.Identity
{
    public class AdmIdentity : User
    {
        public AdmIdentity(int id, string name) : base(id, name)
        {
        }
    }
}
