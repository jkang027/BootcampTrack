using AutoMapper;
using BootcampTrack.Core.Constants;
using BootcampTrack.Core.Domain;
using BootcampTrack.Core.Infrastructure;
using BootcampTrack.Core.Models;
using BootcampTrack.Core.Repository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace BootcampTrack.Api.Controllers
{
    [Authorize(Roles = RoleConstants.Instructor)]
    public class ProjectsController : ApiController
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectsController(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Projects
        public IEnumerable<ProjectModel> GetProjects()
        {
            return Mapper.Map<IEnumerable<ProjectModel>>(_projectRepository.GetAll());
        }

        // GET: api/Projects/5
        [ResponseType(typeof(Project))]
        public IHttpActionResult GetProject(int id)
        {
            Project project = _projectRepository.GetById(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ProjectModel>(project));
        }

        // PUT: api/Projects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProject(int id, ProjectModel project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != project.ProjectId)
            {
                return BadRequest();
            }

            var dbProject = _projectRepository.GetById(id);

            dbProject.Update(project);

            _projectRepository.Update(dbProject);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Projects
        [ResponseType(typeof(Project))]
        public IHttpActionResult PostProject(ProjectModel project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbProject = new Project(project);

            _projectRepository.Add(dbProject);
            _unitOfWork.Commit();

            project.ProjectId = dbProject.ProjectId;

            return CreatedAtRoute("DefaultApi", new { id = project.ProjectId }, project);
        }

        // DELETE: api/Projects/5
        [ResponseType(typeof(Project))]
        public IHttpActionResult DeleteProject(int id)
        {
            Project project = _projectRepository.GetById(id);
            if (project == null)
            {
                return NotFound();
            }

            _projectRepository.Delete(project);
            _unitOfWork.Commit();


            return Ok(Mapper.Map<ProjectModel>(project));
        }

        private bool ProjectExists(int id)
        {
            return _projectRepository.Any(e => e.ProjectId == id);
        }
    }
}