using BootcampTrack.Api.Infrastructure;
using BootcampTrack.Api.Requests;
using BootcampTrack.Core.Constants;
using BootcampTrack.Core.Domain;
using BootcampTrack.Core.Infrastructure;
using BootcampTrack.Core.Repository;
using BootcampTrack.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
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

        [Authorize(Roles = RoleConstants.SchoolAdministrator)]
        [Route("api/invite/instructor")]
        public IHttpActionResult InviteInstructor(InstructorInvitation invite)
        {
            var instructorInvite = new InstructorInvite
            {
                SchoolBranchId = invite.BranchId,
                Token = Security.GetTimeStampedToken()
            };

            _instructorInviteRepository.Add(instructorInvite);
            _unitOfWork.Commit();
            
            var fromAddress = new MailAddress(RoleConstants.BootcampTrackEmail, "Bootcamp Track");
            var toAddress = new MailAddress(invite.EmailAddress);

            var subjectInput = "Invite Code for Bootcamp Track";
            var bodyInput = "Here is your invite code to sign up for Bootcamp Track! Just follow the link." + Environment.NewLine + Environment.NewLine + $"http://localhost:64730/#/inviteinstructor?token={instructorInvite.Token}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(RoleConstants.BootcampTrackEmail, RoleConstants.BootcampTrackEmailPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress) { Subject = subjectInput, Body = bodyInput })
            {
                smtp.Send(message);
            }

            return Ok();
        }

        [Authorize(Roles = RoleConstants.Instructor)]
        [Route("api/invite/student")]
        public IHttpActionResult InviteStudent(StudentInvitation invite)
        {
            var studentInvite = new StudentInvite
            {
                CourseId = invite.CourseId,
                Token = Security.GetTimeStampedToken()
            };

            _studentInviteRepository.Add(studentInvite);
            _unitOfWork.Commit();

            var fromAddress = new MailAddress(RoleConstants.BootcampTrackEmail, "Bootcamp Track");
            var toAddress = new MailAddress(invite.EmailAddress);

            var subjectInput = "Invite Code for Bootcamp Track";
            var bodyInput = "Here is your invite code to sign up for Bootcamp Track! Just follow the link." + Environment.NewLine + Environment.NewLine + $"http://localhost:64730/#/invitestudent?token={studentInvite.Token}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(RoleConstants.BootcampTrackEmail, RoleConstants.BootcampTrackEmailPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress) { Subject = subjectInput, Body = bodyInput })
            {
                smtp.Send(message);
            }

            return Ok();
        }

        [AllowAnonymous]
        [Route("api/invite/verify/instructor/{token}")]
        [HttpGet]
        public IHttpActionResult VerifyInstructorToken(string token)
        {
            if (_instructorInviteRepository.Any(si => si.Token == token))
            {
                return Ok(new {
                    TokenFound = true,
                    TokenExpired = Security.HasTokenExpired(token)
                });
            }
            else
            {
                return NotFound();
            }
        }

        [AllowAnonymous]
        [Route("api/invite/verify/student/{token}")]
        [HttpGet]
        public IHttpActionResult VerifyStudentToken(string token)
        {
            if(_studentInviteRepository.Any(si => si.Token == token))
            {
                return Ok(new
                {
                    TokenFound = true,
                    TokenExpired = Security.HasTokenExpired(token)
                });
            }
            else
            {
                return NotFound();
            }
        }
    }
}