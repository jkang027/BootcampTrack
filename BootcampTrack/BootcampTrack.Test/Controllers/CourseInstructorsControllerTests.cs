﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BootcampTrack.Api.Controllers;
using BootcampTrack.Core.Repository;
using Moq;
using BootcampTrack.Core.Infrastructure;
using System.Web.Http.Results;
using BootcampTrack.Core.Models;
using BootcampTrack.Core.Domain;
using BootcampTrack.Api;
using System.Collections.Generic;

namespace BootcampTrack.Test.Controllers
{
    [TestClass]
    public class CourseInstructorsControllerTests
    {
        [TestMethod]
        public void GetCourseInstructorShouldReturnCourseInstructor()
        {
            //Arrange
            WebApiConfig.CreateMaps();
            var _courseInstructorRepository = new Mock<ICourseInstructorRepository>();
            _courseInstructorRepository.Setup(m => m.GetById(1))
                                       .Returns(new CourseInstructor
                                       {
                                           CourseInstructorId = 1,
                                           CourseId = 1,
                                           InstructorId = "asdf123"
                                       });

            var _unitOfWork = new Mock<IUnitOfWork>();
            var controller = new CourseInstructorsController(_courseInstructorRepository.Object, _unitOfWork.Object);

            //Act
            var httpResponse = controller.GetCourseInstructor(1);
            OkNegotiatedContentResult<CourseInstructorModel> okHttpResponse = (OkNegotiatedContentResult<CourseInstructorModel>)httpResponse;

            //Assert
            Assert.IsNotNull(httpResponse);
            Assert.IsNotNull(okHttpResponse);
            Assert.IsNotNull(okHttpResponse.Content);

            var domainResponse = okHttpResponse.Content;

            Assert.AreEqual(domainResponse.CourseInstructorId, 1);
        }

        [TestMethod]
        public void GetCourseInstructorShouldReturnNotFound()
        {
            //Arrange
            WebApiConfig.CreateMaps();
            var _courseInstructorRepository = new Mock<ICourseInstructorRepository>();
            _courseInstructorRepository.Setup(m => m.GetById(1))
                                       .Returns(new CourseInstructor
                                       {
                                           CourseInstructorId = 1,
                                           CourseId = 1,
                                           InstructorId = "asdf123"
                                       });

            var _unitOfWork = new Mock<IUnitOfWork>();
            var controller = new CourseInstructorsController(_courseInstructorRepository.Object, _unitOfWork.Object);

            //Act
            var httpResponse = controller.GetCourseInstructor(2);

            //Assert
            Assert.IsNotNull(httpResponse);
            Assert.IsInstanceOfType(httpResponse, typeof(NotFoundResult));
        }
    }
}
