using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Models
{
    public class UserModel
    {
        public int? SchoolBranchId { get; set; }
        public string GitHubAccount { get; set; }
        public string LinkedInAccount { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public SchoolBranchModel SchoolBranch { get; set; }

        public class Profile : UserModel
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string EmailAddress { get; set; }
            public string PhoneNumber { get; set; }
        }
    }
}
