using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Models
{
    public class SubmissionModel
    {
        public int SubmissionId { get; set; }
        public string StudentId { get; set; }
        public int ProjectId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comment { get; set; }
    }
}
