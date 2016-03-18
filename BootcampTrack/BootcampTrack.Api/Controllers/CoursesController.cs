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
    public class CoursesController : ApiController
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CoursesController(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Courses
        public IEnumerable<CourseModel> GetCourses()
        {
            return Mapper.Map<IEnumerable<CourseModel>>(_courseRepository.GetAll());
        }

        // GET: api/Courses/5
        [ResponseType(typeof(Course))]
        public IHttpActionResult GetCourse(int id)
        {
            Course course = _courseRepository.GetById(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<CourseModel>(course));
       }

        [Authorize(Roles = RoleConstants.Instructor)]
        // PUT: api/Courses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCourse(int id, CourseModel course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != course.CourseId)
            {
                return BadRequest();
            }

            var dbCourse = _courseRepository.GetById(id);

            dbCourse.Update(course);

            _courseRepository.Update(dbCourse);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!CourseExists(id))
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

        [Authorize(Roles = RoleConstants.Instructor)]
        // POST: api/Courses
        [ResponseType(typeof(Course))]
        public IHttpActionResult PostCourse(CourseModel course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbCourse = new Course(course);

            _courseRepository.Add(dbCourse);
            _unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = course.CourseId }, course);
        }

        [Authorize(Roles = RoleConstants.Instructor)]
        // DELETE: api/Courses/5
        [ResponseType(typeof(Course))]
        public IHttpActionResult DeleteCourse(int id)
        {
            Course course = _courseRepository.GetById(id);
            if (course == null)
            {
                return NotFound();
            }

            _courseRepository.Delete(course);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<CourseModel>(course));
        }

        
        private bool CourseExists(int id)
        {
            return _courseRepository.Any(e => e.CourseId == id);
        }
    }
}