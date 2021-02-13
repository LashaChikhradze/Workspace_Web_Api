using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Workspace_Models.Enums;

namespace Workspace_Models
{
    public class Member : BaseEntity
    {
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int? WorkspaceId { get; set; }
        [ForeignKey("WorkspaceId")]
        public Workspace Workspace { get; set; }
        public UserRole Role { get; set; }
        public MemberStatus Status { get; set; }
    }
}
