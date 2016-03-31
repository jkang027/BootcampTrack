using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Models
{
    public class InstructorInviteModel
    {
        public int InstructorInviteId { get; set; }
        public int SchoolBranchId { get; set; }
        public string Token { get; set; }
        public string EmailAddress { get; set; }
    }
}
