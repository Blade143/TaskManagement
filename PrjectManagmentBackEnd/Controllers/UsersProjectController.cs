using System;
using System.Collections.Generic;
using System.Linq;
using BAL.DTO;
using BAL.Interfaces;
using BLL.Interfaces;
using DAL.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace PrjectManagmentBackEnd.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class UsersProjectController : Controller
    {
        protected IProjectService _service;
        protected ILinkService _lservice;
        protected IRoleService _rservise;
        protected IUserService _uservice;
        protected ITaskService _tservice;

        public UsersProjectController(ITaskService tservice, IRoleService rservice, IProjectService service, ILinkService lservice, IUserService uservice)
        {
            this._service = service;
            this._lservice = lservice;
            this._rservise = rservice;
            this._uservice = uservice;
            this._tservice = tservice;
        }

        // GET: api/<controller>
        [Authorize]
        [HttpGet]
        public ActionResult Get()
        {
            var id = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == "ID").Select(c => c.Value).FirstOrDefault());

            var res = from proj in _service.GetProjects()
                      join link in _lservice.GetLinks() on proj.ProjectId equals link.ProjectId
                      where link.UserId == id
                      join roles in _rservise.GetRoles() on link.RoleId equals roles.RoleId
                      select new
                      {
                          ProjectId = proj.ProjectId,
                          Title = proj.title,
                          Status = proj.status,
                          Image = proj.image,
                          Discription = proj.discription,
                          Role = roles.RoleName
                      };

            return Ok(res);
        }

        [Authorize]
        [HttpGet("{Id}")]
        public ActionResult Get(int id)
        {
            string choise = HttpContext.Request.Headers
                                                    .Where(x => x.Key == "choise")
                                                    .Select(x => x.Value).FirstOrDefault();


            if (choise == "users") {
                var links = _lservice.GetLinks().Where(x => x.ProjectId == id).Select(x => x.UserId).ToList();

                var res = _uservice.GetUsers()
                    .Where(x => links.Contains(x.UserId))
                    .ToList();

                return Ok(res);
            }

            if (choise == "tasks") {
                var tasks = _tservice.GetTasks().Where(x => x.ProjectId == id).ToList();

                return Ok(tasks);
            }

            return Ok();
        }
        // POST api/<controller>
        [Authorize]
        [HttpPost]
        public ActionResult Post([FromBody]TaskDTO task)
        {

            if (task.ParentTaskId == 0) task.ParentTaskId = null;
            task.UserId = null;

            _tservice.AddTask(task);
            _tservice.Save();
            return Ok(true);
        }

        // PUT api/<controller>/5
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Put(int id,[FromBody]ProjectDTO project)
        {
            string choise = HttpContext.Request.Headers
                                                    .Where(x => x.Key == "choise")
                                                    .Select(x => x.Value).FirstOrDefault();

            if (choise == "FireUser") 
            { 
                int userId = Convert.ToInt32(HttpContext.Request.Headers
                                                    .Where(x => x.Key == "userid")
                                                    .Select(x => x.Value).FirstOrDefault());

                var linkid = _lservice.GetLinks().Where(x => x.ProjectId == project.ProjectId && x.UserId == userId).Select(x => x.Id).FirstOrDefault();
                _lservice.Delete(linkid);

                var tasks = _tservice.GetTasks().Where(x => x.ProjectId == project.ProjectId & x.UserId == userId).ToList();

                foreach (var task in tasks)
                {
                    task.UserId = 0;
                    task.Status = "close";

                    _tservice.UpdateTask(task.TaskId, task); 
                }
            }

            if (choise == "SetUser")
            {
                int userId = Convert.ToInt32(HttpContext.Request.Headers
                                                    .Where(x => x.Key == "userid")
                                                    .Select(x => x.Value).FirstOrDefault());

                TaskDTO task = _tservice.GetTask(id);
                task.UserId = userId;

                _tservice.UpdateTask(task.TaskId ,task);
            }

            if (choise == "not")
            {
                _service.UpdateProject(project.ProjectId, project);
                return Ok(project);
            }
            return Ok();
        }

        // DELETE api/<controller>/5
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            string choise = HttpContext.Request.Headers
                                                    .Where(x => x.Key == "choise")
                                                    .Select(x => x.Value).FirstOrDefault();

            if (choise == "link") 
            {
                var userid = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == "ID").Select(c => c.Value).FirstOrDefault());

                var link = _lservice.GetLinks().Where(x => x.UserId == userid && x.ProjectId == id).FirstOrDefault();
                _lservice.Delete(link.Id);
            }
            if (choise == "project")
            {
                var links = _lservice.GetLinks().Where(x => x.ProjectId == id).ToList();

                foreach (var l in links)
                {
                    _lservice.Delete(l.Id);
                }

                _service.Delete(id);
            }

            return Ok(true);
        }
    }
}
