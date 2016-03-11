using BootcampTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Domain
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CourseTopic> CourseTopics { get; set; }

        public Topic()
        {

        }

        public Topic(TopicModel model)
        {
            this.Update(model);
        }

        public void Update(TopicModel model)
        {
            TopicId = model.TopicId;
            Name = model.Name;
        }
    }
}
