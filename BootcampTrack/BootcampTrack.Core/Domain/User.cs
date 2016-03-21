using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampTrack.Core.Models;

namespace BootcampTrack.Core.Domain
{
    public class User : IUser<string>
    {
        public string Id { get; set; }
        public int? SchoolBranchId { get; set; }
        public string UserName { get; set; }
        
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string GitHubAccount { get; set; }
        public string LinkedInAccount { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
        public virtual ICollection<CourseInstructor> CourseInstructors { get; set; }
        public virtual SchoolBranch SchoolBranch { get; set; }
        public virtual School School { get; set; }

        public User()
        {
            Roles = new Collection<UserRole>();
            Enrollments = new Collection<Enrollment>();
            Submissions = new Collection<Submission>();
            CourseInstructors = new Collection<CourseInstructor>();
        }

        public void Update(UserModel.Profile model)
        {
            FirstName = model.FirstName;
            MiddleName = model.MiddleName;
            LastName = model.LastName;
            EmailAddress = model.EmailAddress;
            PhoneNumber = model.PhoneNumber;
            GitHubAccount = model.GitHubAccount;
            LinkedInAccount = model.LinkedInAccount;
        }
    }
}   