using System.Collections.Generic;
using BAL.DTO;
using BAL.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace PrjectManagmentBackEnd.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class RoleController : Controller
    {
        protected IRoleService _service;

        public RoleController(IRoleService service)
        {
            this._service = service;
        }

        // GET: api/Role
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_service.GetRoles());
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_service.GetRole(id));
        }

        // POST: api/Role
        [HttpPost]
        public ActionResult Post([FromBody] RoleDTO role)
        {
            _service.AddRole(role);
            return Ok();
        }

        // PUT: api/Role/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] RoleDTO role)
        {
            _service.UpdateRole(id, role);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok(true);
        }
    }
}
