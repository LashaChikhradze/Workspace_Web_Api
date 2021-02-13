using System;
using System.Collections.Generic;
using System.Text;
using Workspace_Models.Enums;

namespace Workspace_Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public UserRole Role { get; set; }
        public TimeSpan Timezone { get; set; } = TimeZoneInfo.Local.BaseUtcOffset;
    }
}
