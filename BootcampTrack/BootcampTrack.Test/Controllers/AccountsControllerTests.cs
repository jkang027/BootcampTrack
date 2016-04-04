using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BootcampTrack.Api.Controllers;
using Moq;
using BootcampTrack.Core.Infrastructure;
using BootcampTrack.Core.Repository;
using BootcampTrack.Core.Models;
using System.Web.Http.Results;
using System.Threading.Tasks;

namespace BootcampTrack.Test.Controllers
{
    [TestClass]
    public class AccountsControllerTests
    {
        [TestMethod]
        public async Task RegisterShouldRegisterUser()
        {
            //Arrange
            var _authRepository = new Mock<IAuthorizationRepository>();
            var _unitOfWork = new Mock<IUnitOfWork>();
            var _schoolRepository = new Mock<ISchoolRepository>();
            var _schoolBranchRepository = new Mock<ISchoolBranchRepository>();
            var _courseRepository = new Mock<ICourseRepository>();
            var _enrollmentRepository = new Mock<IEnrollmentRepository>();
            var _projectRepository = new Mock<IProjectRepository>();
            var _userRepository = new Mock<IUserRepository>();

            var controller = new AccountsController(_authRepository.Object, _userRepository.Object, _schoolRepository.Object, _enrollmentRepository.Object, _schoolBranchRepository.Object, _courseRepository.Object, _projectRepository.Object, _unitOfWork.Object);

            //Act
            var registration = new RegistrationModel.SchoolAdministrator
            {
                Username = "junkang",
                Password = "password",
                ConfirmPassword = "password",
                EmailAddress = "jun@kang.com",
                FirstName = "jun",
                MiddleName = "young",
                LastName = "kang",
                SchoolName = "origin",
                City = "san diego",
                State = "CA"
            };

            var response = await controller.Register(registration);

            //Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }
    }
}
