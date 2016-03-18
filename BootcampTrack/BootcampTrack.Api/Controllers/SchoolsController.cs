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
        public IHttpActionResult GetSchool(string id)
        {
            School school = _schoolRepository.GetById(id);
            if (school == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<SchoolModel>(school));
        }

        // PUT: api/Schools/5
        [Authorize(Roles = RoleConstants.SchoolAdministrator)]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSchool(string id, SchoolModel school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != school.SchoolAdministratorId)
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

        private bool SchoolExists(string id)
        {
            return _schoolRepository.Any(e => e.SchoolAdministratorId == id);
        }
    }
}