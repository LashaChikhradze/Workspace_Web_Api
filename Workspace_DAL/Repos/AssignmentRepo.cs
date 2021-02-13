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
    public class AssignmentRepo : IAssignmentRepo
    {
        private ApplicationDbContext _dbContext = default;
        public AssignmentRepo(ApplicationDbContext db)
        {
            _dbContext = db;
        }
        public IEnumerable<Assignment> AssignmentsToDo(int workspaceId, int userId)
        {
            var member = _dbContext.Members.FirstOrDefault(o => o.WorkspaceId == workspaceId && o.UserId == userId && o.Status == 0);
            if(member != null)
            {
                return _dbContext.Assignments.Where(o => o.RecieverId == member.UserId && o.Cancel == 0).ToList();
            }
            return null;
        }

        public bool CancelAssignment(int workspaceId, int userId, int id, Assignment value)
        {
            var member = _dbContext.Members.FirstOrDefault(o => o.WorkspaceId == workspaceId && o.UserId == userId && o.Status == 0);
            if (member != null)
            {
                var assignment = SentAssignments(workspaceId, userId).FirstOrDefault(o => o.Id == id);
                if (assignment != null)
                {
                    assignment.Cancel = value.Cancel;
                    _dbContext.Assignments.Update(assignment);
                    return SaveChanges();
                }
            }
            return false;
        }

        public bool GiveAssignment(int workspaceId, int userId, int id, Assignment value)
        {
            if(userId != id)
            {
                var Creatormember = _dbContext.Members.FirstOrDefault(o => o.WorkspaceId == workspaceId && o.UserId == userId && o.Status == 0);
                var Recievermember = _dbContext.Members.FirstOrDefault(o => o.WorkspaceId == workspaceId && o.UserId == id && o.Status == 0);
                Assignment response = null;
                if (Creatormember != null && Recievermember != null)
                {
                    response = new Assignment()
                    {
                        Sender = _dbContext.Users.FirstOrDefault(o => o.Id == userId),
                        Reciever = _dbContext.Users.FirstOrDefault(o => o.Id == id),
                        Text = value.Text,
                        Status = TaskStatus.ToDo,
                        Cancel = TaskCancel.IsActive
                    };
                    _dbContext.Assignments.Add(response);
                    return SaveChanges();
                }
            }
            return false;
        }

        public bool ManageAssignment(int workspaceId, int userId, int id, Assignment value)
        {
            var member = _dbContext.Members.FirstOrDefault(o => o.WorkspaceId == workspaceId && o.UserId == userId && o.Status == 0);
            if(member != null)
            {
                var assignment = SearchById(workspaceId, userId, id);
                if(assignment != null)
                {
                    assignment.Status = value.Status;
                    _dbContext.Assignments.Update(assignment);
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
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Assignment SearchById(int workspaceId, int userId, int id)
        {
            return AssignmentsToDo(workspaceId, userId).FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<Assignment> SentAssignments(int workspaceId, int userId)
        {
            var member = _dbContext.Members.FirstOrDefault(o => o.WorkspaceId == workspaceId && o.UserId == userId && o.Status == 0);
            if (member != null)
            {
                return _dbContext.Assignments.Where(o => o.SenderId == member.UserId && o.Cancel == 0).ToList();
            }
            return null;
        }
    }
}
