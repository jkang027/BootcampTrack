using AutoMapper;
using BootcampTrack.Api.Infrastructure;
using BootcampTrack.Core.Constants;
using BootcampTrack.Core.Domain;
using BootcampTrack.Core.Infrastructure;
using BootcampTrack.Core.Models;
using BootcampTrack.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace BootcampTrack.Api.Controllers
{
    public class AccountsController : BaseApiController
    {
        private readonly IAuthorizationRepository _authRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISchoolRepository _schoolRepository;
        private readonly ISchoolBranchRepository _schoolBranchRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public AccountsController(IAuthorizationRepository authRepository, IUserRepository userRepository, ISchoolRepository schoolRepository, IEnrollmentRepository enrollmentRepository, ISchoolBranchRepository schoolBranchRepository, ICourseRepository courseRepository, IUnitOfWork unitOfWork) : base(userRepository)
        {
            _authRepository = authRepository;
            _unitOfWork = unitOfWork;
            _schoolBranchRepository = schoolBranchRepository;
            _schoolRepository = schoolRepository;
            _courseRepository = courseRepository;
            _enrollmentRepository = enrollmentRepository;
        }

        [AllowAnonymous]
        [Route("api/accounts/register")]
        public async Task<IHttpActionResult> Register(RegistrationModel.SchoolAdministrator registration)
        {
            //Server Side Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Pass the Registration onto AuthRepository
            var result = await _authRepository.RegisterUser(registration);

            //Check to see the Registration was Successful
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Registration form was invalid.");
            }
        }

        [AllowAnonymous]
        [Route("api/accounts/register/instructor")]
        public async Task<IHttpActionResult> RegisterInstructor(RegistrationModel.Instructor registration)
        {
            //Server Side Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Pass the Registration onto AuthRepository
            var result = await _authRepository.RegisterInstructor(registration);

            //Check to see the Registration was Successful
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Registration form was invalid.");
            }
        }

        [AllowAnonymous]
        [Route("api/accounts/register/student")]
        public async Task<IHttpActionResult> RegisterStudent(RegistrationModel.Student registration)
        {
            //Server Side Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Pass the Registration onto AuthRepository
            var result = await _authRepository.RegisterStudent(registration);

            //Check to see the Registration was Successful
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Registration form was invalid.");
            }
        }
        
        // GET: api/user/school
        [Authorize(Roles = RoleConstants.SchoolAdministrator)]
        [Route("api/user/school")]
        public IEnumerable<SchoolModel> GetUserSchool()
        {
            return Mapper.Map<IEnumerable<SchoolModel>>(_schoolRepository.GetWhere(s => s.SchoolAdministratorId == CurrentUser.Id));
        }

        // GET: api/user/schoolbranches
        [Authorize(Roles = RoleConstants.SchoolAdministrator)]
        [Route("api/user/schoolbranches")]
        [HttpGet]
        public IEnumerable<SchoolBranchModel> GetUserSchoolBranches()
        {
            return Mapper.Map<IEnumerable<SchoolBranchModel>>(_schoolBranchRepository.GetWhere(sb => sb.School.SchoolAdministratorId == CurrentUser.Id));
        }

        // GET: api/user/courses
        [Authorize(Roles = RoleConstants.SchoolAdministrator)]
        [Route("api/user/courses")]
        [HttpGet]
        public IEnumerable<CourseModel> GetUserCourses()
        {
            return Mapper.Map<IEnumerable<CourseModel>>(_courseRepository.GetWhere(c => c.SchoolBranch.School.SchoolAdministratorId == CurrentUser.Id));
        }

        // GET: api/user/enrollments
        [Authorize(Roles = RoleConstants.SchoolAdministrator)]
        [Route("api/user/enrollments")]
        [HttpGet]
        public IEnumerable<EnrollmentModel> GetUserEnrollments()
        {
            return Mapper.Map<IEnumerable<EnrollmentModel>>(_enrollmentRepository.GetWhere(e => e.Course.SchoolBranch.School.SchoolAdministratorId == CurrentUser.Id));
        }
        
        //TODO: Profile Stuff
        //[Route("api/accounts/currentuser")]
        //[HttpGet]
        //[ResponseType(typeof(UserModel))]
        //public IHttpActionResult GetCurrentUser()
        //{
        //    return Ok(Mapper.Map<UserModel>(CurrentUser));
        //}
    }
}