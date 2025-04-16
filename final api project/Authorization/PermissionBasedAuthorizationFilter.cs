using final_api_project.DBcontext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace final_api_project.Authorization
{
    public class PermissionBasedAuthorizationFilter(UniversityContext dbcontext) : IAuthorizationFilter
    {
        void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
        {
            var attribute = (CheckPermissionAttribute)context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is CheckPermissionAttribute);
            if (attribute != null)
            {
                var claimIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
                if (claimIdentity == null || !claimIdentity.IsAuthenticated)
                {
                    context.Result = new ForbidResult();
                }
                else
                {
                    var userid = int.Parse(context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var haspermission = dbcontext.UserPermissions.Any(x => x.Userid == userid &&
                    x.Permissionid == (int)attribute.Permission);
                    if (!haspermission)
                    {
                        context.Result = new ForbidResult();
                    }
                }
            }
        }
    }
}
