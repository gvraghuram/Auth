using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace NASSAuth.RoleStore
{
    public interface IBaseRoleProvider
    {
        #region Public Methods
        Task<ICollection<string>> GetUserRolesAsync(string userName);
        #endregion
    }
}   

