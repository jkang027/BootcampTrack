using BootcampTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Domain
{
    public class Submission
    {
        public int SubmissionId { get; set; }
        public string StudentId { get; set; }
        public int ProjectId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comment { get; set; }
        
        public virtual Project Project { get; set; }
        public virtual User Student { get; set; }

        public Submission()
        {

        }

        public Submission(SubmissionModel model)
        {
            Update(model);
            CreatedDate = DateTime.Now;
        }

        public void Update(SubmissionModel model)
        {
            SubmissionId = model.SubmissionId;
            StudentId = model.StudentId;
            ProjectId = model.ProjectId;
            CreatedDate = model.CreatedDate;
            Comment = model.Comment;
        }
    }
}