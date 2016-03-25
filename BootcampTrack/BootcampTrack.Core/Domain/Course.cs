using BootcampTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Domain
{
    public class Course
    {
        public int CourseId { get; set; }
        public int SchoolBranchId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? MaxEnrollments { get; set; }
        public int NumberOfEnrollments
        {
            get
            {
                return Enrollments.Count();
            }
        }

        public virtual ICollection<CourseTopic> CourseTopics { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<CourseInstructor> CourseInstructors { get; set; }
        public virtual ICollection<StudentInvite> StudentInvites { get; set; }
        public virtual SchoolBranch SchoolBranch { get; set; }

        public Course()
        {
            CourseTopics = new Collection<CourseTopic>();
            Enrollments = new Collection<Enrollment>();
            Projects = new Collection<Project>();
            CourseInstructors = new Collection<CourseInstructor>();
            StudentInvites = new Collection<StudentInvite>();
        }

        public Course(CourseModel model)
        {
            Update(model);
        }

        public void Update(CourseModel model)
        {
            CourseId = model.CourseId;
            SchoolBranchId = model.SchoolBranchId;
            Address1 = model.Address1;
            Address2 = model.Address2;
            Address3 = model.Address3;
            City = model.City;
            State = model.State;
            ZipCode = model.ZipCode;
            StartDate = model.StartDate;
            EndDate = model.EndDate;
            MaxEnrollments = model.MaxEnrollments;
        }
    }
}
