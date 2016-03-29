using AutoMapper;
using BootcampTrack.Api.Infrastructure;
using BootcampTrack.Core.Models;
using BootcampTrack.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BootcampTrack.Api.Controllers
{
    /*
    
        DashboardService.getUserSchool()
            .then(function (userSchoolResp) {
                $scope.school = userSchoolResp;
                return DashboardService.getUserSchoolBranches();

            }).then(function (userSchoolBranchResp) {
                $scope.branches = userSchoolBranchResp;
                return DashboardService.getUserCourses();

            }).then(function (userCoursesResp) {
                $scope.courses = userCoursesResp;
                return DashboardService.getUserEnrollments();

            }).then(function (enrollmentsResp) {
                $scope.enrollments = enrollmentsResp;
                return DashboardService.getSchoolProjects();

            }).then(function (schoolProjectsResp) {
                $scope.projects = schoolProjectsResp;
                $scope.initializeDone = true;

            }).catch(function (errorResponse) {
                $scope.error = errorResponse;
            });
    }

    */
    public class DashboardController : BaseApiController
    {
        private readonly ISchoolRepository _schoolRepository;

        public DashboardController(ISchoolRepository schoolRepository, IUserRepository userRepository) : base(userRepository)
        {
            _schoolRepository = schoolRepository;
        }

        [Route("api/dashboard")]
        public IHttpActionResult GetDashboard()
        {
            return Ok(new
            {
                School = Mapper.Map<SchoolModel>(CurrentUser.School),
                BranchCount = CurrentUser.School.SchoolBranches.Count,
                CourseCount = CurrentUser.School.SchoolBranches.Sum(sb => sb.Courses.Count),
                EnrollmentCount = CurrentUser.School.SchoolBranches.Sum(sb => sb.Courses.Sum(c => c.Enrollments.Count)),
                ProjectCount = CurrentUser.School.SchoolBranches.Sum(sb => sb.Courses.Sum(c => c.Projects.Count))
            });
        }

        public bool CheckCoursesEqualServer(int courseCount)
        {
            return _schoolRepository.Count(s => s.SchoolAdministratorId == CurrentUser.Id) == courseCount;
        }
    }
}
