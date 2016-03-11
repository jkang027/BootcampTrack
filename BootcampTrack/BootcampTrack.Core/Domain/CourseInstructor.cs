using BootcampTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Domain
{
    public class CourseInstructor
    {
        public int CourseInstructorId { get; set; }
        public int CourseId { get; set; }
        public string InstructorId { get; set; }

        public virtual User Instructor { get; set; }
        public virtual Course Course { get; set; }

        public CourseInstructor()
        {

        }

        public CourseInstructor(CourseInstructorModel model)
        {
            Update(model);
        }

        public void Update(CourseInstructorModel model)
        {
            CourseInstructorId = model.CourseInstructorId;
            InstructorId = model.InstructorId;
            CourseId = model.CourseId;
        }
    }
}
