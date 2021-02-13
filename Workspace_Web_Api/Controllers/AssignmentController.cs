using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workspace_DAL.Repos.Abstraction;
using Workspace_Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Workspace_Web_Api.Controllers
{
    [Route("api/{workspaceId}/{userId}/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentRepo _assignmentRepo = default;
        public AssignmentController(IAssignmentRepo assignmentRepository)
        {
            _assignmentRepo = assignmentRepository;
        }
        // GET: api/<AssignmentController>
        [HttpGet("ToDo")]
        public IEnumerable<Assignment> Get(int workspaceId, int userId)
        {
            return _assignmentRepo.AssignmentsToDo(workspaceId, userId);
        }

        // GET api/<AssignmentController>/5
        [HttpGet("ToDo/{id}")]
        public Assignment Get(int workspaceId, int userId, int id)
        {
            return _assignmentRepo.SearchById(workspaceId, userId, id);
        }
        // GET: api/<AssignmentController>

        [HttpGet("{sent}")]
        public IEnumerable<Assignment> Get(int workspaceId, int userId, string sent)
        {
            if(sent == "Sent")
                return _assignmentRepo.SentAssignments(workspaceId, userId);
            return null;
        }

        // POST api/<AssignmentController>
        [HttpPost("Create/{id}")]
        public IActionResult Post(int workspaceId, int userId, int id, [FromBody] Assignment value)
        {
            if (value == null) { return BadRequest(); }
            var response = _assignmentRepo.GiveAssignment(workspaceId, userId, id, value);
            if (response)
                return Ok(value);
            return BadRequest();
        }

        // PUT api/<AssignmentController>/5
        [HttpPut("Manage/{id}")]
        public IActionResult Put(int workspaceId, int userId, int id, [FromBody] Assignment value)
        {
            if (value == null) { return BadRequest(); }
            var response = _assignmentRepo.ManageAssignment(workspaceId, userId, id, value);
            if (response)
                return Ok(value);
            return BadRequest();
        }

        // DELETE api/<AssignmentController>/5
        [HttpDelete("Cancel/{id}")]
        public IActionResult Delete(int workspaceId, int userId, int id, [FromBody] Assignment value)
        {
            if (value == null) { return BadRequest(); }
            var response = _assignmentRepo.CancelAssignment(workspaceId, userId, id, value);
            if (response)
                return Ok(value);
            return BadRequest();
        }
    }
}
