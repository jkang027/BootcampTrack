using AutoMapper;
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
    [Authorize]
    public class SubmissionsController : ApiController
    {
        private readonly ISubmissionRepository _submissionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubmissionsController(ISubmissionRepository submissionRepository, IUnitOfWork unitOfWork)
        {
            _submissionRepository = submissionRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Submissions
        public IEnumerable<SubmissionModel> GetSubmissions()
        {
            return Mapper.Map<IEnumerable<SubmissionModel>>(_submissionRepository.GetAll());
        }

        // GET: api/Submissions/5
        [ResponseType(typeof(Submission))]
        public IHttpActionResult GetSubmission(int id)
        {
            Submission submission = _submissionRepository.GetById(id);
            if (submission == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<SubmissionModel>(submission));
        }

        [Authorize(Roles = RoleConstants.Student)]
        // PUT: api/Submissions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubmission(int id, SubmissionModel submission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != submission.SubmissionId)
            {
                return BadRequest();
            }

            var dbSubmission = _submissionRepository.GetById(id);

            dbSubmission.Update(submission);
            _submissionRepository.Update(dbSubmission);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!SubmissionExists(id))
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

        [Authorize(Roles = RoleConstants.Student)]
        // POST: api/Submissions
        [ResponseType(typeof(Submission))]
        public IHttpActionResult PostSubmission(SubmissionModel submission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbSubmission = new Submission(submission);

            _submissionRepository.Add(dbSubmission);
            _unitOfWork.Commit();

            submission.SubmissionId = dbSubmission.SubmissionId;

            return CreatedAtRoute("DefaultApi", new { id = submission.SubmissionId }, submission);
        }

        // DELETE: api/Submissions/5
        [ResponseType(typeof(Submission))]
        public IHttpActionResult DeleteSubmission(int id)
        {
            Submission submission = _submissionRepository.GetById(id);
            if (submission == null)
            {
                return NotFound();
            }

            _submissionRepository.Delete(submission);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<SubmissionModel>(submission));
        }
        
        private bool SubmissionExists(int id)
        {
            return _submissionRepository.Any(e => e.SubmissionId == id);
        }
    }
}