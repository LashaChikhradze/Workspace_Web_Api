using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Workspace_DAL.DB;
using Workspace_DAL.Repos.Abstraction;
using Workspace_Models;
using Workspace_Models.Enums;

namespace Workspace_DAL.Repos
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _dbContext = default;
        public UserRepository(ApplicationDbContext db)
        {
            _dbContext = db;
        }
        public IEnumerable<User> Read ()
        {
            return _dbContext.Users.ToList();
        }
        public User SearchById(int id)
        {
            return _dbContext.Users.FirstOrDefault(o => o.Id == id);
        }
        public bool CreateNewUser(User item)
        {
            _dbContext.Users.Add(item);
            return SaveChanges();
        }

        public bool CreateNewWorkspace(int id, Workspace item)
        {
            _dbContext.Workspaces.Add(item);
            var user = _dbContext.Users.FirstOrDefault(o => o.Id == id);
            var tmp = new Member()
            {
                User = user,
                Workspace = item,
                Role = UserRole.Admin
            };
            _dbContext.Members.Add(tmp);
            return SaveChanges();
        }

        public bool AddMemberToWorkspace(int adderId, int userId, int workspaceId)
        {
            var workspaces = GetUserWorkspaces(adderId).ToList();
            foreach(var wp in workspaces)
            {
                if(wp.Id == workspaceId)
                {
                    var user = _dbContext.Users.FirstOrDefault(o => o.Id == userId);
                    var workspace = _dbContext.Workspaces.FirstOrDefault(o => o.Id == workspaceId);
                    var item = new Member()
                    {
                        User = user,
                        Workspace = workspace,
                        Role = UserRole.Member
                    };
                    _dbContext.Members.Add(item);
                    return SaveChanges();
                }
            }
            return false;
        }

        public bool SaveChanges()
        {
            try
            {
                return _dbContext.SaveChanges() >= 0;
            }catch(Exception)
            {
                return false;
            }
        }

        public IEnumerable<Workspace> GetUserWorkspaces(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(o => o.Id == id);
            List<Member> members = new List<Member>();
            if (user != null)
            {
                members = _dbContext.Members.Where(o => o.UserId == id && o.Status == 0).ToList();
            }
            List<Workspace> workspaces = new List<Workspace>();
            if (members != null)
            {
                foreach (var item in members)
                {
                    var wp = _dbContext.Workspaces.FirstOrDefault(o => o.Id == item.WorkspaceId);
                    workspaces.Add(wp);
                }
                return workspaces;
            }
            return null;
        }

        public IEnumerable<Workspace> GetAdminWorkspaces(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(o => o.Id == id);
            List<Member> members = new List<Member>();
            if(user != null)
            {
                members = _dbContext.Members.Where(o => o.UserId == id && o.Role == 0 && o.Status == 0).ToList();
            }
            List<Workspace> workspaces = new List<Workspace>();
            if(members != null)
            {
                foreach (var item in members)
                {
                    var wp = _dbContext.Workspaces.FirstOrDefault(o => o.Id == item.WorkspaceId);
                    workspaces.Add(wp);
                }
                return workspaces;
            }
            return default;
        }
        public bool ManageMemberRole(int adminId, int workspaceId, int userId, Member item)
        {
            var workspaces = GetAdminWorkspaces(adminId).ToList();
            foreach(var wp in workspaces)
            {
                if(wp.Id == workspaceId)
                {
                    var member = _dbContext.Members.FirstOrDefault(o => o.WorkspaceId == wp.Id && o.UserId == userId && o.Status == 0);
                    if(member != null)
                    {
                        member.Role = item.Role;
                        _dbContext.Members.Update(member);
                        return SaveChanges();
                    }
                }
            }
            return false;
        }
        public IEnumerable<User> SearchWorkspaceMembersByName(int adminId, int workspaceId, string name)
        {
            var result = new List<User>();
            var workspaces = GetAdminWorkspaces(adminId).ToList();
            foreach(var wp in workspaces)
            {
                if(wp.Id == workspaceId)
                {
                    var members = new List<Member>();
                    var users = _dbContext.Users.Where(o => o.Name == name);
                    foreach(var item in users)
                    {
                        var member = _dbContext.Members.FirstOrDefault(o => o.WorkspaceId == wp.Id && o.UserId == item.Id && o.Status == 0);
                        if(member != null)
                        {
                            members.Add(member);
                        }
                    }
                    foreach(var mb in members)
                    {
                        if(mb.User.Name == name)
                        {
                            result.Add(mb.User);
                        }
                    }
                    return result;
                }
            }
            return null;
        }
        public bool KickWorkspaceMember(int adminId, int workspaceId, int userId)
        {
            var workspaces = GetAdminWorkspaces(adminId).ToList();
            foreach (var wp in workspaces)
            {
                if (wp.Id == workspaceId)
                {
                    var member = _dbContext.Members.FirstOrDefault(o => o.WorkspaceId == wp.Id && o.UserId == userId);
                    if(member != null)
                    {
                        member.Status = MemberStatus.IsDeleted;
                        _dbContext.Members.Update(member);
                        return SaveChanges();
                    }
                }
            }
            return false;
        }
    }
}
