using BootcampTrack.Core.Constants;
using BootcampTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Domain
{
    public class School
    {
        public int SchoolId { get; set; }
        public string SchoolAdministratorId { get; set; }

        public string SchoolName { get; set; }
        public string SchoolAdministratorName
        {
            get
            {
                return SchoolAdministrator?.LastName + ", " + SchoolAdministrator?.FirstName;
            }
        }
        public string SchoolDescription { get; set; }
        public string SchoolEmailAddress { get; set; }
        public string SchoolPhoneNumber { get; set; }
        public string Website { get; set; }
        public bool ShowCoursesPublic { get; set; }
        public DateTime? SchoolEstablishedDate { get; set; }

        public virtual ICollection<SchoolBranch> SchoolBranches { get; set; }
        public virtual User SchoolAdministrator { get; set; }
      
        public School()
        {
            SchoolBranches = new Collection<SchoolBranch>();
        }

        public School(SchoolModel model)
        {
            Update(model);
        }

        public void Update(SchoolModel model)
        {
            SchoolId = model.SchoolId;
            SchoolName = model.SchoolName;
            SchoolDescription = model.SchoolDescription;
            SchoolEmailAddress = model.SchoolEmailAddress;
            SchoolPhoneNumber = model.SchoolPhoneNumber;
            Website = model.Website;
            SchoolEstablishedDate = model.SchoolEstablishedDate;
            ShowCoursesPublic = model.ShowCoursesPublic;
        }
    }
}