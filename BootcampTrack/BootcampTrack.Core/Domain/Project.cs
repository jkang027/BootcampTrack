using BootcampTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Domain
{
    public class Project
    {
        public int ProjectId { get; set; }
        public int CourseId { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<Submission> Submissions { get; set; }
        public virtual Course Course { get; set; }

        public Project()
        {
            Submissions = new Collection<Submission>();
        }

        public Project(ProjectModel model)
        {
            Update(model);
        }

        public void Update(ProjectModel model)
        {
            ProjectId = model.ProjectId;
            CourseId = model.CourseId;
            StartDate = model.StartDate;
            DueDate = model.DueDate;
            Description = model.Description;
        }
    }
}
