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
    public class EnrollmentsController : ApiController
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EnrollmentsController(IEnrollmentRepository enrollmentRepository, IUnitOfWork unitOfWork)
        {
            _enrollmentRepository = enrollmentRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Enrollments
        public IEnumerable<EnrollmentModel> GetEnrollments()
        {
            return Mapper.Map<IEnumerable<EnrollmentModel>>(_enrollmentRepository.GetAll());
        }

        // GET: api/Enrollments/5
        [ResponseType(typeof(Enrollment))]
        public IHttpActionResult GetEnrollment(int id)
        {
            Enrollment enrollment = _enrollmentRepository.GetById(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<EnrollmentModel>(enrollment));
        }

        // PUT: api/Enrollments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEnrollment(int id, EnrollmentModel enrollment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != enrollment.EnrollmentId)
            {
                return BadRequest();
            }

            var dbEnrollment = _enrollmentRepository.GetById(id);

            dbEnrollment.Update(enrollment);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!EnrollmentExists(id))
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

        // POST: api/Enrollments
        [ResponseType(typeof(Enrollment))]
        public IHttpActionResult PostEnrollment(EnrollmentModel enrollment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbEnrollment = new Enrollment(enrollment);

            _enrollmentRepository.Add(dbEnrollment);
            _unitOfWork.Commit();

            enrollment.EnrollmentId = dbEnrollment.EnrollmentId;

            return CreatedAtRoute("DefaultApi", new { id = enrollment.EnrollmentId }, enrollment);
        }

        // DELETE: api/Enrollments/5
        [ResponseType(typeof(Enrollment))]
        public IHttpActionResult DeleteEnrollment(int id)
        {
            Enrollment enrollment = _enrollmentRepository.GetById(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            _enrollmentRepository.Delete(enrollment);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<EnrollmentModel>(enrollment));
        }

        private bool EnrollmentExists(int id)
        {
            return _enrollmentRepository.Any(e => e.EnrollmentId == id);
        }
    }
}