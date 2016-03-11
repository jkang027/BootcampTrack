using BootcampTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Domain
{
    public class CourseTopic
    {
        public int CourseTopicId { get; set; }
        public int CourseId { get; set; }
        public int TopicId { get; set; }
        
        public virtual Course Course { get; set; }
        public virtual Topic Topic { get; set; }

        public CourseTopic()
        {

        }

        public CourseTopic(CourseTopicModel model)
        {
            Update(model);
        }

        public void Update(CourseTopicModel model)
        {
            CourseTopicId = model.CourseTopicId;
            CourseId = model.CourseId;
            TopicId = model.TopicId;
        }
    }
}
