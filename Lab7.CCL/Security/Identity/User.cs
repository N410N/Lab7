using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.CCL.Security.Identity
{
    public abstract class User
    {
        protected User(int id, string name)
        {
            Id = id; Name = name;
        }
        int Id { get; set; }
        public string Name { get; set; }
    }
}
