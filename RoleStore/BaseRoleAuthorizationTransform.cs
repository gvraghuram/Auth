using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace NASSAuth.RoleStore
{
    public class BaseRoleAuthorizationTransform : IClaimsTransformation
    {
        #region Private Fields
        private static readonly string RoleClaimType = $"http://{typeof(BaseRoleAuthorizationTransform).FullName.Replace('.', '/')}/role";
        private readonly IBaseRoleProvider _roleProvider;
        #endregion
        #region Public Constructors
        public BaseRoleAuthorizationTransform(IBaseRoleProvider roleProvider)
        {
            _roleProvider = roleProvider ?? throw new ArgumentNullException(nameof(roleProvider));
        }
        #endregion
        #region Public Methods
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            // Cast the principal identity to a Claims identity to access claims etc...
            var oldIdentity = (ClaimsIdentity)principal.Identity;

            // "Clone" the old identity to avoid nasty side effects.
            // NB: We take a chance to replace the claim type used to define the roles with our own.
            var newIdentity = new ClaimsIdentity(oldIdentity.Claims, oldIdentity.AuthenticationType, oldIdentity.NameClaimType, RoleClaimType);

            // Fetch the roles for the user and add the claims of the correct type so that roles can be recognized.
            var roles = await _roleProvider.GetUserRolesAsync(newIdentity.Name);
            newIdentity.AddClaims(roles.Select(r => new Claim(RoleClaimType, r)));

            // Create and return a new claims principal
            return new ClaimsPrincipal(newIdentity);
        }
        #endregion
    }
}
