using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Domain
{
    public class InstructorInvite
    {
        public int InstructorInviteId { get; set; }
        public int SchoolId { get; set; }
        public string Token { get; set; }

        public virtual School School { get; set; }
    }
}