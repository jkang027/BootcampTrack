using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Domain
{
    public class StudentInvite
    {
        public int StudentInviteId { get; set; }
        public int CourseId { get; set; }
        public string Token { get; set; }

        public virtual Course Course { get; set; }
    }
}