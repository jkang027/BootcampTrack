﻿using AutoMapper;
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
    public class TopicsController : ApiController
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TopicsController(ITopicRepository topicRepository, IUnitOfWork unitOfWork)
        {
            _topicRepository = topicRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Topics
        public IEnumerable<TopicModel> GetTopics()
        {
            //return Mapper.Map<IEnumerable<TopicModel>>(db.Topics);
            return Mapper.Map<IEnumerable<TopicModel>>(_topicRepository.GetAll());
        }

        // GET: api/Topics/5
        [ResponseType(typeof(Topic))]
        public IHttpActionResult GetTopic(int id)
        {
            //Topic topic = db.Topics.Find(id);
            Topic topic = _topicRepository.GetById(id);
            if (topic == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<TopicModel>(topic));
        }

        // PUT: api/Topics/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTopic(int id, TopicModel topic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != topic.TopicId)
            {
                return BadRequest();
            }

            //var dbTopic = db.Topics.Find(id);
            var dbTopic = _topicRepository.GetById(id);

            dbTopic.Update(topic);

            // db.Entry(topic).State = EntityState.Modified;
            _topicRepository.Update(dbTopic);

            try
            {
                //  db.SaveChanges();
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!TopicExists(id))
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

        // DELETE: api/Topics/5
        [ResponseType(typeof(Topic))]
        public IHttpActionResult DeleteTopic(int id)
        {
            // Topic topic = db.Topics.Find(id);
            Topic topic = _topicRepository.GetById(id);

            if (topic == null)
            {
                return NotFound();
            }

            //db.Topics.Remove(topic);
            //db.SaveChanges();

            _topicRepository.Delete(topic);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<TopicModel>(topic));
        }

        private bool TopicExists(int id)
        {
            //return db.Topics.Count(e => e.TopicId == id) > 0;
            return _topicRepository.Any(e => e.TopicId == id);
        }
    }
}