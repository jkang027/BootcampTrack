using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BootcampTrack.Core.Domain;
using BootcampTrack.Data.Infrastructure;
using BootcampTrack.Core.Constants;
using BootcampTrack.Core.Repository;
using BootcampTrack.Core.Infrastructure;
using AutoMapper;
using BootcampTrack.Core.Models;

namespace BootcampTrack.Api.Controllers
{
    [Authorize(Roles = RoleConstants.SchoolAdministrator)]
    public class SchoolBranchesController : ApiController
    {
        private readonly ISchoolBranchRepository _schoolBranchRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SchoolBranchesController(ISchoolBranchRepository schoolBranchRepository, IUnitOfWork unitOfWork)
        {
            _schoolBranchRepository = schoolBranchRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/SchoolBranches
        public IEnumerable<SchoolBranchModel> GetSchoolBranches()
        {
            return Mapper.Map<IEnumerable<SchoolBranchModel>>(_schoolBranchRepository.GetAll());
        }

        // GET: api/SchoolBranches/5
        [ResponseType(typeof(SchoolBranch))]
        public IHttpActionResult GetSchoolBranch(int id)
        {
            SchoolBranch schoolBranch = _schoolBranchRepository.GetById(id);
            if (schoolBranch == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<SchoolBranchModel>(schoolBranch));
        }

        // PUT: api/SchoolBranches/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSchoolBranch(int id, SchoolBranchModel schoolBranch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != schoolBranch.SchoolBranchId)
            {
                return BadRequest();
            }

            var dbSchoolBranch = _schoolBranchRepository.GetById(id);

            dbSchoolBranch.Update(schoolBranch);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!SchoolBranchExists(id))
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

        // POST: api/SchoolBranches
        [ResponseType(typeof(SchoolBranch))]
        public IHttpActionResult PostSchoolBranch(SchoolBranchModel schoolBranch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbSchoolBranch = new SchoolBranch(schoolBranch);

            _schoolBranchRepository.Add(dbSchoolBranch);
            _unitOfWork.Commit();

            schoolBranch.SchoolBranchId = dbSchoolBranch.SchoolBranchId;

            return CreatedAtRoute("DefaultApi", new { id = schoolBranch.SchoolBranchId }, schoolBranch);
        }

        // DELETE: api/SchoolBranches/5
        [ResponseType(typeof(SchoolBranch))]
        public IHttpActionResult DeleteSchoolBranch(int id)
        {
            SchoolBranch schoolBranch = _schoolBranchRepository.GetById(id);
            if (schoolBranch == null)
            {
                return NotFound();
            }

            _schoolBranchRepository.Delete(schoolBranch);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<SchoolBranchModel>(schoolBranch));
        }

        private bool SchoolBranchExists(int id)
        {
            return _schoolBranchRepository.Any(e => e.SchoolBranchId == id);
        }
    }
}