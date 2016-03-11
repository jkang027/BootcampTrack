using BootcampTrack.Core.Constants;
using BootcampTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Domain
{
    public class School
    {
        public int SchoolId { get; set; }
        public string SchoolAdministratorId { get; set; }
        public string SchoolAdministratorName
        {
            get
            {
                return SchoolAdministrator?.LastName + ", " + SchoolAdministrator?.FirstName;
            }
        }
        public string SchoolName { get; set; }
        public string SchoolDescription { get; set; }
        public string SchoolEmailAddress { get; set; }
        public string SchoolPhoneNumber { get; set; }
        public string Website { get; set; }
        public DateTime? SchoolEstablishedDate { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public User SchoolAdministrator
        {
            get
            {
                return Users.FirstOrDefault(u => u.Roles.Any(r => r.Role.Name == RoleConstants.SchoolAdministrator));
            }
        }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<InstructorInvite> InstructorInvites { get; set; }

        public School()
        {

        }

        public School(SchoolModel model)
        {
            Update(model);
        }

        public void Update(SchoolModel model)
        {
            SchoolId = model.SchoolId;
            SchoolAdministratorId = model.SchoolAdministratorId;
            SchoolName = model.SchoolName;
            SchoolDescription = model.SchoolDescription;
            SchoolEmailAddress = model.SchoolEmailAddress;
            SchoolPhoneNumber = model.SchoolPhoneNumber;
            Website = model.Website;
            SchoolEstablishedDate = model.SchoolEstablishedDate;
            Address1 = model.Address1;
            Address2 = model.Address2;
            Address3 = model.Address3;
            City = model.City;
            State = model.State;
            ZipCode = model.ZipCode;
        }
    }
}