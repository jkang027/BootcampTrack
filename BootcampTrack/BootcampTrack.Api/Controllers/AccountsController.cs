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
        [HttpGet]
        public SchoolModel GetUserSchool()
        {
            return Mapper.Map<SchoolModel>(_schoolRepository.GetById(CurrentUser.Id));
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
        [Authorize(Roles = RoleConstants.SchoolAdministrator + "," + RoleConstants.Instructor)]
        [Route("api/user/courses")]
        [HttpGet]
        public IEnumerable<CourseModel> GetUserCourses()
        {
            return Mapper.Map<IEnumerable<CourseModel>>(_courseRepository.GetWhere(c => c.SchoolBranch.School.SchoolAdministratorId == CurrentUser.Id));
        }

        // GET: api/user/enrollments
        [Authorize(Roles = RoleConstants.SchoolAdministrator + "," + RoleConstants.Instructor)]
        [Route("api/user/enrollments")]
        [HttpGet]
        public IEnumerable<EnrollmentModel> GetUserEnrollments()
        {
            return Mapper.Map<IEnumerable<EnrollmentModel>>(_enrollmentRepository.GetWhere(e => e.Course.SchoolBranch.School.SchoolAdministratorId == CurrentUser.Id));
        }

        // GET: api/user/profile
        [Authorize]
        [Route("api/user/profile")]
        [HttpGet]
        public IHttpActionResult GetCurrentUser()
        {
            return Ok(Mapper.Map<UserModel.Profile>(CurrentUser));
        }

        // PUT: api/user/profile
        [Authorize]
        [Route("api/user/profile")]
        [HttpPut]
        public IHttpActionResult UpdateCurrentUser(string id, UserModel.Profile user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (id != CurrentUser.Id)
            {
                return BadRequest();
            }

            var dbUserProfile = _userRepository.GetById(id);

            dbUserProfile.Update(user);
            _userRepository.Update(dbUserProfile);
            
            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!UserExists(id))
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

        private bool UserExists(string id)
        {
            return _userRepository.Any(e => e.Id == id);
        }
    }
}