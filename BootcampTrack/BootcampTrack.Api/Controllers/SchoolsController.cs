using AutoMapper;
using BootcampTrack.Api.Infrastructure;
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
    [Authorize(Roles = RoleConstants.SchoolAdministrator)]
    public class SchoolsController : ApiController
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SchoolsController(ISchoolRepository schoolRepository, IUnitOfWork unitOfWork)
        {
            _schoolRepository = schoolRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Schools
        public IEnumerable<SchoolModel> GetSchools()
        {
            return Mapper.Map<IEnumerable<SchoolModel>>(_schoolRepository.GetAll());
        }

        // GET: api/Schools/5
        [ResponseType(typeof(School))]
        public IHttpActionResult GetSchool(int id)
        {
            School school = _schoolRepository.GetById(id);
            if (school == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<SchoolModel>(school));
        }

        // PUT: api/Schools/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSchool(int id, SchoolModel school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != school.SchoolId)
            {
                return BadRequest();
            }

            var dbSchool = _schoolRepository.GetById(id);

            dbSchool.Update(school);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!SchoolExists(id))
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

        // POST: api/Schools
        [ResponseType(typeof(School))]
        public IHttpActionResult PostSchool(SchoolModel school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbSchool = new School(school);

            _schoolRepository.Add(dbSchool);
            _unitOfWork.Commit();

            school.SchoolId = dbSchool.SchoolId;

            return CreatedAtRoute("DefaultApi", new { id = school.SchoolId }, school);
        }

        // DELETE: api/Schools/5
        [ResponseType(typeof(School))]
        public IHttpActionResult DeleteSchool(int id)
        {
            School school = _schoolRepository.GetById(id);
            if (school == null)
            {
                return NotFound();
            }

            _schoolRepository.Delete(school);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<SchoolModel>(school));
        }

        private bool SchoolExists(int id)
        {
            return _schoolRepository.Any(e => e.SchoolId == id);
        }
    }
}