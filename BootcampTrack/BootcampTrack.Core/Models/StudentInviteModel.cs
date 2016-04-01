using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Models
{
    public class StudentInviteModel
    {
        public int StudentInviteId { get; set; }
        public int CourseId { get; set; }
        public string Token { get; set; }
        public string EmailAddress { get; set; }
    }
}
