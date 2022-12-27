using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NASSAuth.RoleStore
{
    public class RoleProvider : IBaseRoleProvider
    {
        public const string ADMIN = "Admin";
        public const string BASIC_USER = "BasicUser";

        public Task<ICollection<string>> GetUserRolesAsync(string userName)
        {
            ICollection<string> result = new string[0];

            if (!string.IsNullOrEmpty(userName))
            // TODO persistence
            // username + roles
            {
                result = new[] { BASIC_USER, ADMIN };
                //result = new[] { BASIC_USER };
            }

            return Task.FromResult(result);
        }

    }
}
