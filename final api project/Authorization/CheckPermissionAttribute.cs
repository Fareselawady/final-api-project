using final_api_project.Models;

namespace final_api_project.Authorization
{
    [AttributeUsage(AttributeTargets.Method , AllowMultiple =false)]
    public class CheckPermissionAttribute : Attribute
    {
        public CheckPermissionAttribute(Permission permission)
        {
            Permission = permission;
        }
        public Permission Permission { get; set; }
    }
}
