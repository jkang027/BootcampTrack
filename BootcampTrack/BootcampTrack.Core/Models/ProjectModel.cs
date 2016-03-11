using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Models
{
    public class ProjectModel
    {
        public int ProjectId { get; set; }
        public int CourseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string Description { get; set; }
    }
}
