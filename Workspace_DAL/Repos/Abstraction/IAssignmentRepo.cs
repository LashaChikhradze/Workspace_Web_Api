using System;
using System.Collections.Generic;
using System.Text;
using Workspace_Models;

namespace Workspace_DAL.Repos.Abstraction
{
    public interface IAssignmentRepo
    {
        IEnumerable<Assignment> AssignmentsToDo(int workspaceId, int userId);
        IEnumerable<Assignment> SentAssignments(int workspaceId, int userId);
        Assignment SearchById(int workspaceId, int userId, int id);
        bool GiveAssignment(int workspaceId, int userId, int id, Assignment value);
        bool ManageAssignment(int workspaceId, int userId, int id, Assignment value);
        bool CancelAssignment(int workspaceId, int userId, int id, Assignment value);
        bool SaveChanges();
    }
}
