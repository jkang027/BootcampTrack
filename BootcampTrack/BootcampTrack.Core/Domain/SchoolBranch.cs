using BootcampTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Domain
{
    public class SchoolBranch
    {
        public int SchoolBranchId { get; set; }
        public string SchoolAdministratorId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public virtual ICollection<User> Instructors { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<InstructorInvite> InstructorInvites { get; set; }

        public virtual School School { get; set; }

        public SchoolBranch()
        {
            Instructors = new Collection<User>();
            Courses = new Collection<Course>();
            InstructorInvites = new Collection<InstructorInvite>();
        }

        public SchoolBranch(SchoolBranchModel model)
        {
            Update(model);
        }

        public void Update(SchoolBranchModel model)
        {
            SchoolBranchId = model.SchoolBranchId;
            SchoolAdministratorId = model.SchoolAdministratorId;
            Address1 = model.Address1;
            Address2 = model.Address2;
            Address3 = model.Address3;
            City = model.City;
            State = model.State;
            ZipCode = model.ZipCode;
        }
    }
}
