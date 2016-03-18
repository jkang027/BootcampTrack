using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Models
{
    public class SchoolModel
    {
        public string SchoolAdministratorId { get; set; }
        public string SchoolAdministratorName { get; set; }
        public string SchoolName { get; set; }
        public string SchoolDescription { get; set; }
        public string SchoolEmailAddress { get; set; }
        public string SchoolPhoneNumber { get; set; }
        public bool ShowCoursesPublic { get; set; }
        public string Website { get; set; }
        public DateTime? SchoolEstablishedDate { get; set; }
    }
}
