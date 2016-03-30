using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootcampTrack.Api.Requests
{
    public class StudentInvitation
    {
        public string EmailAddress { get; set; }
        public int CourseId { get; set; }
    }
}