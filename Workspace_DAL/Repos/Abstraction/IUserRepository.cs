using System;
using System.Collections.Generic;
using System.Text;
using Workspace_Models;

namespace Workspace_DAL.Repos.Abstraction
{
    public interface IUserRepository
    {
        IEnumerable<User> Read();
        User SearchById(int id);
        bool CreateNewUser(User item);
        IEnumerable<Workspace> GetUserWorkspaces(int id);
        IEnumerable<Workspace> GetAdminWorkspaces(int id);
        bool CreateNewWorkspace(int id, Workspace item);
        bool AddMemberToWorkspace(int adderId, int userId, int workspaceId);
        bool ManageMemberRole(int adminId, int workspaceId, int userId, Member item);
        IEnumerable<User> SearchWorkspaceMembersByName(int adminId, int workspaceId, string name);
        bool KickWorkspaceMember(int adminId, int workspaceId, int userId);
        bool SaveChanges();
    }
}
