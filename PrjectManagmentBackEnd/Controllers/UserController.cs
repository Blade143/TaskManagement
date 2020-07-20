using System;
using System.Collections.Generic;
using System.Linq;
using BAL.DTO;
using BAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace PrjectManagmentBackEnd.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class UserController : Controller
    {
        protected IUserService _service;

        public UserController(IUserService service)
        {
            this._service = service;
        }

        // GET: api/User/5
        [Authorize]
        [HttpGet]
        public ActionResult Get()
        {
            var id = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == "ID").Select(c => c.Value).FirstOrDefault());
            return Ok(_service.GetUser(id));
        }

        // PUT: api/User/5
        [Authorize]
        [HttpPut]
        public ActionResult Put( [FromBody] UserDTO user)
        {
            int id = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == "ID").Select(c => c.Value).FirstOrDefault());
            _service.UpdateUser(id, user);
            return Ok(user);
        }

        // POST: api/User/5
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Post([FromBody] UserDTO user)
        {
            _service.AddUser(user);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var userid = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == "ID").Select(c => c.Value).FirstOrDefault());
            if (userid == id) {
                _service.Delete(id);
                return Ok(true);
            }
            return BadRequest(false);
        }
    }
}
