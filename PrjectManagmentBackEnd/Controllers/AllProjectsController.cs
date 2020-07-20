using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAL.DTO;
using BAL.Interfaces;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PrjectManagmentBackEnd.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class AllProjectsController : Controller
    {
        protected IProjectService _pserviese;
        protected ILinkService _lservice;

        public AllProjectsController(IProjectService pservice, ILinkService lservice)
        {
            this._pserviese = pservice;
            this._lservice = lservice;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Get()
        {
            var id = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == "ID").Select(c => c.Value).FirstOrDefault());

            var projetIds = _lservice.GetLinks().Where(x => x.UserId == id).Select(x => x.ProjectId).Distinct().ToList();
            var projects = _pserviese.GetProjects().Where(x => !projetIds.Contains(x.ProjectId)).ToList();
            return Ok(projects);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Post([FromBody] ProjectDTO project)
        {
            var id = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == "ID").Select(c => c.Value).FirstOrDefault());

            if (project != null)
            {
                _pserviese.AddProject(project);
                int pid = _pserviese.GetProjectByTitle(project.title).ProjectId;
                _lservice.AddLink(new ProjectUserRoleDTO()
                {
                    UserId = id,
                    ProjectId = pid,
                    RoleId = 1
                });
                return Ok(project);
            }
            else 
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPut]
        public ActionResult Put([FromBody] int ProjectId)
        {
            var id = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == "ID").Select(c => c.Value).FirstOrDefault());

            _lservice.AddLink(new ProjectUserRoleDTO()
            {
                UserId = id,
                ProjectId = ProjectId,
                RoleId = 3
            });
            return Ok(true);
        }

    }
}