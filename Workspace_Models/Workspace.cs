using System;
using System.Collections.Generic;
using System.Text;
using Workspace_Models.Enums;

namespace Workspace_Models
{
    public class Workspace : BaseEntity
    {
        public string Heading { get; set; }
        public string Description { get; set; }
        public WorkspaceStatus Status { get; set; }
        public TimeSpan Timezone { get; set; } = TimeZoneInfo.Utc.BaseUtcOffset;
    }
}
