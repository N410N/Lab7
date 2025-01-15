using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab7.CCL.Security.Identity;

namespace Lab7.CCL
{
    public  static class SecurityContext
    {
        private static User _user;

        public static void SetUser(User user)
        {
            _user = user;
        }
        public static User GetUser()
        {
            if (_user == null)
            {
                throw new NullReferenceException("Не знайдено поточного користувача.");
            }
            return _user;
        }
    }
}
