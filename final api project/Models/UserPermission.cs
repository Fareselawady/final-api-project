using System;
using System.Collections.Generic;

namespace final_api_project.Models;

public partial class UserPermission
{
    public int Userid { get; set; }

    public int Permissionid { get; set; }

    public virtual User User { get; set; } = null!;
}
