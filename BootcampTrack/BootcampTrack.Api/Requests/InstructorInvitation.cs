using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootcampTrack.Api.Requests
{
    public class InstructorInvitation
    {
        public string EmailAddress { get; set; }
        public int BranchId { get; set; }
    }
}