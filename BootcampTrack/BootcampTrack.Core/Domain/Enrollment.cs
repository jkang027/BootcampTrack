using BootcampTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Domain
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public string StudentId { get; set; }
        public int CourseId { get; set; }
        
        public virtual Course Course { get; set; }
        public virtual User Student { get; set; }

        public Enrollment()
        {

        }

        public Enrollment(EnrollmentModel model)
        {
            Update(model);
        }

        public void Update(EnrollmentModel model)
        {
            EnrollmentId = model.EnrollmentId;
            StudentId = model.StudentId;
            CourseId = model.CourseId;
        }
    }
}