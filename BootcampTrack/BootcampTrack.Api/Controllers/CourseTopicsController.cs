using AutoMapper;
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
    public class CourseTopicsController : ApiController
    {
        private readonly ICourseTopicRepository _courseTopicRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CourseTopicsController(ICourseTopicRepository courseTopicRepository, IUnitOfWork unitOfWork)
        {
            _courseTopicRepository = courseTopicRepository;
            _unitOfWork = unitOfWork;
        }


        // GET: api/CourseTopics
        public IEnumerable<CourseTopicModel> GetCourseTopics()
        {
            return Mapper.Map<IEnumerable<CourseTopicModel>>(_courseTopicRepository.GetAll());
        }

        // GET: api/CourseTopics/5
        [ResponseType(typeof(CourseTopic))]
        public IHttpActionResult GetCourseTopic(int id)
        {
            CourseTopic courseTopic = _courseTopicRepository.GetById(id);
            if (courseTopic == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<CourseTopicModel>(courseTopic));
        }

        // PUT: api/CourseTopics/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCourseTopic(int id, CourseTopicModel courseTopic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != courseTopic.CourseTopicId)
            {
                return BadRequest();
            }

            var dbCourseTopic = _courseTopicRepository.GetById(id);

            dbCourseTopic.Update(courseTopic);

            _courseTopicRepository.Update(dbCourseTopic);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!CourseTopicExists(id))
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

        // POST: api/CourseTopics
        [ResponseType(typeof(CourseTopic))]
        public IHttpActionResult PostCourseTopic(CourseTopicModel courseTopic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbCourseTopic = new CourseTopic(courseTopic);

            _courseTopicRepository.Add(dbCourseTopic);
            _unitOfWork.Commit();

            courseTopic.CourseTopicId = dbCourseTopic.CourseTopicId;

            return CreatedAtRoute("DefaultApi", new { id = courseTopic.CourseTopicId }, courseTopic);
        }

        // DELETE: api/CourseTopics/5
        [ResponseType(typeof(CourseTopic))]
        public IHttpActionResult DeleteCourseTopic(int id)
        {
            CourseTopic courseTopic = _courseTopicRepository.GetById(id);
            if (courseTopic == null)
            {
                return NotFound();
            }

            _courseTopicRepository.Delete(courseTopic);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<CourseTopicModel>(courseTopic));
        }

        private bool CourseTopicExists(int id)
        {
            return _courseTopicRepository.Any(e => e.CourseTopicId == id);
        }
    }
}