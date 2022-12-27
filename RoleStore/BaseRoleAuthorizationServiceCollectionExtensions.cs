using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace NASSAuth.RoleStore
{
    /// <summary>
    /// Provides the extension methods to enable and register the simple role authentication on an Asp.Net Core web site.
    /// </summary>
    public static class BaseRoleAuthorizationServiceCollectionExtensions
{
    #region Public Static Methods
    /// <summary>
    /// Activates simple role authorization for Windows authentication for the ASP.Net Core web site.
    /// </summary>
    /// <typeparam name="TRoleProvider">The <see cref="Type"/> of the <see cref="IBaseRoleProvider"/> implementation that will provide user roles.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> onto which to register the services.</param>
    public static void AddRoleAuthorization<TRoleProvider>(this IServiceCollection services) where TRoleProvider : class, IBaseRoleProvider
    {
        services.AddSingleton<IBaseRoleProvider, TRoleProvider>();
        services.AddSingleton<IClaimsTransformation, BaseRoleAuthorizationTransform>();
    }
    #endregion
}
}