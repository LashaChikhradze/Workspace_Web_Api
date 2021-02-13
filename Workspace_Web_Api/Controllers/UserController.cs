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
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository = default;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userRepository.Read();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userRepository.SearchById(id);
        }
        [HttpGet("{id}/{workspaces}/All")]
        public IEnumerable<Workspace> Get(int id,string workspaces)
        {
            if(workspaces == "Workspaces")
            {
                return _userRepository.GetUserWorkspaces(id);
            }
            return null;
        }
        [HttpGet("{id}/{workspaces}/{admin}")]
        public IEnumerable<Workspace> Get(int id, string workspaces, string admin)
        {
            if (workspaces == "Workspaces" && admin == "Admin")
            {
                return _userRepository.GetAdminWorkspaces(id);
            }
            return null;
        }
        [HttpGet("{adminId}/{workspaceId}/Search/{name}")]
        public IEnumerable<User> Get(int adminId, int workspaceId, string name)
        {
            return _userRepository.SearchWorkspaceMembersByName(adminId, workspaceId, name);
        }
        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] User value)
        {
            if(value == null) { return BadRequest(); }
            var response = _userRepository.CreateNewUser(value);
            if (response)
                return Ok(value);
            return BadRequest();
        }
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody] Workspace value)
        {
            if (value == null) { return BadRequest(); }
            var response = _userRepository.CreateNewWorkspace(id, value);
            if (response)
                return Ok(value);
            return BadRequest();
        }

        [HttpPost("{adderId}/{workspaceId}/Add/{userId}")]
        public bool Post(int adderId, int workspaceId, int userId)
        {
            var response = _userRepository.AddMemberToWorkspace(adderId, userId, workspaceId);
            return response;
        }

        // PUT api/<UserController>/5
        [HttpPut("{adminId}/{workspaceId}/{userId}")]
        public IActionResult Put(int adminId, int workspaceId, int userId, [FromBody] Member value)
        {
            if(value == null) { return BadRequest(); }
            var response = _userRepository.ManageMemberRole(adminId, workspaceId, userId, value);
            if (response)
                return Ok(value);
            return BadRequest();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{adminId}/{workspaceId}/Kick/{userId}")]
        public IActionResult Delete(int adminId, int workspaceId, int userId)
        {
            var result = _userRepository.KickWorkspaceMember(adminId, workspaceId, userId);
            if (result)
                return Ok(true);
            return BadRequest();
        }
    }
}
