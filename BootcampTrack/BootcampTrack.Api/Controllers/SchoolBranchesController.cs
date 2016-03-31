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
using BootcampTrack.Api.Infrastructure;

namespace BootcampTrack.Api.Controllers
{
    public class SchoolBranchesController : BaseApiController
    {
        private readonly ISchoolBranchRepository _schoolBranchRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseRepository _courseRepository;
        private readonly IInstructorInviteRepository _instructorInviteRepository;

        public SchoolBranchesController(IUserRepository userRepository, ISchoolBranchRepository schoolBranchRepository, ICourseRepository courseRepository, IInstructorInviteRepository instructorInviteRepository, IUnitOfWork unitOfWork) : base(userRepository)
        {
            _schoolBranchRepository = schoolBranchRepository;
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
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

        //GET: api/SchoolBranches/5/Courses
        [ResponseType(typeof(Course))]
        [Route("api/schoolbranches/{id}/courses")]
        public IEnumerable<CourseModel> GetSchoolBranchCourses(int id)
        {
            return Mapper.Map<IEnumerable<CourseModel>>(_courseRepository.GetWhere(c => c.SchoolBranchId == id));
        }

        //GET: api/SchoolBranches/5/InstructorInvites
        [ResponseType(typeof(InstructorInvite))]
        [Route("api/schoolbranches/{id}/courses")]
        public IEnumerable<InstructorInvite> GetSchoolBranchInstructorInvites(int id)
        {
            return Mapper.Map<IEnumerable<InstructorInvite>>(_instructorInviteRepository.GetWhere(ii => ii.SchoolBranchId == id));
        }

        [Authorize(Roles = RoleConstants.SchoolAdministrator)]
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
            _schoolBranchRepository.Update(dbSchoolBranch);

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

        [Authorize(Roles = RoleConstants.SchoolAdministrator)]
        // POST: api/SchoolBranches
        [ResponseType(typeof(SchoolBranch))]
        public IHttpActionResult PostSchoolBranch(SchoolBranchModel schoolBranch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbSchoolBranch = new SchoolBranch(schoolBranch);

            dbSchoolBranch.SchoolAdministratorId = CurrentUser.Id;

            _schoolBranchRepository.Add(dbSchoolBranch);
            _unitOfWork.Commit();

            schoolBranch.SchoolBranchId = dbSchoolBranch.SchoolBranchId;

            return CreatedAtRoute("DefaultApi", new { id = schoolBranch.SchoolBranchId }, schoolBranch);
        }

        [Authorize(Roles = RoleConstants.SchoolAdministrator)]
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