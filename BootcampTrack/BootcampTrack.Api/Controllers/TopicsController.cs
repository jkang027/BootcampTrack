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
    public class TopicsController : ApiController
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TopicsController(ITopicRepository topicRepository, IUnitOfWork unitOfWork)
        {
            _topicRepository = topicRepository;
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        // GET: api/Topics
        public IEnumerable<TopicModel> GetTopics()
        {
            //return Mapper.Map<IEnumerable<TopicModel>>(db.Topics);
            return Mapper.Map<IEnumerable<TopicModel>>(_topicRepository.GetAll());
        }

        [Authorize(Roles = RoleConstants.SchoolAdministrator + "," + RoleConstants.Instructor)]
        // POST: api/Topics
        [ResponseType(typeof(Topic))]
        public IHttpActionResult PostTopic(TopicModel topic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbTopic = new Topic(topic);

            // db.Topics.Add(dbTopic);
            // db.SaveChanges();

            _topicRepository.Add(dbTopic);
            _unitOfWork.Commit();

            topic.TopicId = dbTopic.TopicId;

            return CreatedAtRoute("DefaultApi", new { id = topic.TopicId }, topic);
        }

        private bool TopicExists(int id)
        {
            //return db.Topics.Count(e => e.TopicId == id) > 0;
            return _topicRepository.Any(e => e.TopicId == id);
        }
    }
}