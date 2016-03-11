using BootcampTrack.Api.Infrastructure;
using BootcampTrack.Core.Domain;
using BootcampTrack.Core.Infrastructure;
using BootcampTrack.Core.Repository;
using BootcampTrack.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BootcampTrack.Api.Controllers
{
    [Authorize]
    public class InviteController : BaseApiController
    {
        private readonly IInstructorInviteRepository _instructorInviteRepository;
        private readonly IStudentInviteRepository _studentInviteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InviteController(IInstructorInviteRepository instructorInviteRepository, IStudentInviteRepository studentInviteRepository, IUserRepository userRepository, IUnitOfWork unitOfWork) : base(userRepository)
        {
            _instructorInviteRepository = instructorInviteRepository;
            _studentInviteRepository = studentInviteRepository;
            _unitOfWork = unitOfWork;
        }

        [Route("api/invite/instructor")]
        public IHttpActionResult InviteInstructor(string emailAddress)
        {
            var instructorInvite = new InstructorInvite
            {
                SchoolId = CurrentUser.School.SchoolId,
                Token = Security.GetTimeStampedToken()
            };

            _instructorInviteRepository.Add(instructorInvite);
            _unitOfWork.Commit();

            //System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            //message.To.Add(emailAddress);
            //message.Subject = "This is the your Invite Code for " + CurrentUser.School.SchoolName + ".";
            //message.From = new System.Net.Mail.MailAddress(CurrentUser.School.SchoolEmailAddress);
            //message.Body = "You've been invited as an instructor for " + CurrentUser.School.SchoolName + ". Click the following link to register for an account. http://www.bootcamptrack.com/#/invite/instructor?token=instructorInvite.Token";
            //System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("yoursmtphost");
            //smtp.Send(message);
     
            //TODO: Send an email to emailAddress w/ the following link
            //T0D0: Create invite state in app.js
            //Http://www.bootcamptrack.com/#/invite/instructor?token=instructorInvite.Token

            return Ok();
        }

        [Route("api/invite/student")]
        public IHttpActionResult InviteStudent(string emailAddress, int courseId)
        {
            var studentInvite = new StudentInvite
            {
                CourseId = courseId,
                Token = Security.GetTimeStampedToken()
            };

            _studentInviteRepository.Add(studentInvite);
            _unitOfWork.Commit();

            //TODO: Send an email to emailAddress w/ the following link
            //T0D0: Create invite state in app.js
            //Http://www.bootcamptrack.com/#/invite/student?token=studentInvite.Token

            return Ok();
        }
    }
}
