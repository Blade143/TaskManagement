using System.Collections.Generic;
using BAL.DTO;
using BAL.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System;

namespace PrjectManagmentBackEnd.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [EnableCors("CorsPolicy")]
    public class UserTaskController : Controller
    {
        protected ITaskService _tservice;
        protected IUserService _uservice;
        protected IProjectService _pservice;

        public UserTaskController(ITaskService tservice, IUserService uservise, IProjectService pservice)
        {
            this._tservice = tservice;
            this._uservice = uservise;
            this._pservice = pservice;
        }
        // GET: api/Task/Id
        [Authorize]
        [HttpGet("{Id}")]
        public ActionResult Get(int Id)
        {
            var userid = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == "ID").Select(c => c.Value).FirstOrDefault());

            string choise = HttpContext.Request.Headers
                                                    .Where(x => x.Key == "choise")
                                                    .Select(x => x.Value).FirstOrDefault();
            if (choise == "user")
            {
                var result = from task in _tservice.GetTasks()
                             join project in _pservice.GetProjects() on task.ProjectId equals project.ProjectId
                             where project.ProjectId == Id
                             join user in _uservice.GetUsers() on task.UserId equals user.UserId
                             where user.UserId == userid
                             select new
                             {
                                 TaskId = task.TaskId,
                                 UserId = task.UserId,
                                 UserName = user.UserName,
                                 ProjectId = task.ProjectId,
                                 ProjectTitle = project.title,
                                 TaskParentID = task.ParentTaskId,
                                 TaskName = task.TaskName,
                                 TaskDiscritpion = task.Disription,
                                 TaskStatus = task.Status,
                                 TaskCreateDate = task.CreateDate,
                                 RatingPoints = task.RatingPoints,
                                 TaskEstimeteDate = task.EstimateDate
                             };
                return Ok(result);
            }

            if (choise == "project")
            {
                var result = _tservice.GetTasks().Where(x => !x.UserId.HasValue).Join(_pservice.GetProjects(), t => t.ProjectId, p => p.ProjectId, (t, p) => new
                {
                    TaskId = t.TaskId,
                    ProjectId = t.ProjectId,
                    ProjectTitle = p.title,
                    TaskParentID = t.ParentTaskId,
                    TaskName = t.TaskName,
                    TaskDiscritpion = t.Disription,
                    TaskStatus = t.Status,
                    TaskCreateDate = t.CreateDate,
                    RatingPoints = t.RatingPoints,
                    TaskEstimeteDate = t.EstimateDate
                }).ToList();


                return Ok(result);
            }

            return Ok();
        }

        // POST: api/Task
        [Authorize]
        [HttpPost]
        public ActionResult Post([FromBody] TaskDTO task)
        {
            var userid = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == "ID").Select(c => c.Value).FirstOrDefault());
            task.UserId = userid;
            _tservice.UpdateTask(task.TaskId, task);
            return Ok(true);
        }

        // PUT: api/Task/5
        [Authorize]
        [HttpPut]
        public ActionResult Put([FromBody] TaskDTO task)
        {
            var userid = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == "ID").Select(c => c.Value).FirstOrDefault());
            var user = _uservice.GetUserNoTraking(userid);

            if (user.Rating == 0)
            {
                user.Rating += Convert.ToInt32(task.RatingPoints);
            }
            else
            {
                user.Rating += task.RatingPoints.Value / (user.Rating / 2);
            }
            task.Status = "finished";
            _tservice.UpdateTask(task.TaskId, task);
            _uservice.UpdateUser(user.UserId, user);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var userid = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == "ID").Select(c => c.Value).FirstOrDefault());
            var user = _uservice.GetUserNoTraking(userid);

            user.Rating -= user.Rating / 10;

            var task = _tservice.GetTask(id);
            task.UserId = null;
            _tservice.UpdateTask(task.TaskId, task);
            _uservice.UpdateUser(user.UserId, user);
            return Ok();
        }
    }
}
