using System;
using System.Collections.Generic;
using System.Text;

namespace Workspace_Models.Enums
{
    public enum UserRole { Admin, Member};
    public enum WorkspaceStatus { Active, Achieved};
    public enum TaskStatus { ToDo, InProgress, Done};
    public enum MemberStatus { IsActive, IsDeleted};
    public enum TaskCancel { IsActive, IsDeleted};
}
