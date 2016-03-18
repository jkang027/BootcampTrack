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
    [Authorize]
    public class CourseInstructorsController : ApiController
    {
        private readonly ICourseInstructorRepository _courseInstructorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CourseInstructorsController(ICourseInstructorRepository courseInstructorRepository, IUnitOfWork unitOfWork)
        {
            _courseInstructorRepository = courseInstructorRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/CourseInstructors
        public IEnumerable<CourseInstructorModel> GetCourseInstructors()
        {
            return Mapper.Map<IEnumerable<CourseInstructorModel>>(_courseInstructorRepository.GetAll());
        }

        // GET: api/CourseInstructors/5
        [ResponseType(typeof(CourseInstructor))]
        public IHttpActionResult GetCourseInstructor(int id)
        {
            CourseInstructor courseInstructor = _courseInstructorRepository.GetById(id);
            if (courseInstructor == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<CourseInstructorModel>(courseInstructor));
        }

        [Authorize(Roles = RoleConstants.SchoolAdministrator)]
        // PUT: api/CourseInstructors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCourseInstructor(int id, CourseInstructorModel courseInstructor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != courseInstructor.CourseInstructorId)
            {
                return BadRequest();
            }

            var dbCourseInstructor = _courseInstructorRepository.GetById(id);

            dbCourseInstructor.Update(courseInstructor);

            _courseInstructorRepository.Update(dbCourseInstructor);

            try
            {
                _unitOfWork.Commit();
            }

            catch (Exception)
            {
                if (!CourseInstructorExists(id))
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

        [Authorize(Roles = RoleConstants.SchoolAdministrator)]
        // POST: api/CourseInstructors
        [ResponseType(typeof(CourseInstructor))]
        public IHttpActionResult PostCourseInstructor(CourseInstructorModel courseInstructor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbCourseInstructor = new CourseInstructor(courseInstructor);

            _courseInstructorRepository.Add(dbCourseInstructor);
            _unitOfWork.Commit();

            courseInstructor.CourseInstructorId = dbCourseInstructor.CourseInstructorId;

            return CreatedAtRoute("DefaultApi", new { id = courseInstructor.CourseInstructorId }, courseInstructor);
        }

        [Authorize(Roles = RoleConstants.SchoolAdministrator)]
        // DELETE: api/CourseInstructors/5
        [ResponseType(typeof(CourseInstructor))]
        public IHttpActionResult DeleteCourseInstructor(int id)
        {
            CourseInstructor courseInstructor = _courseInstructorRepository.GetById(id);

            if (courseInstructor == null)
            {
                return NotFound();
            }

            _courseInstructorRepository.Delete(courseInstructor);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<CourseInstructorModel>(courseInstructor));
        }
        
        private bool CourseInstructorExists(int id)
        {
            return _courseInstructorRepository.Any(e => e.CourseInstructorId == id);
        }
    }
}