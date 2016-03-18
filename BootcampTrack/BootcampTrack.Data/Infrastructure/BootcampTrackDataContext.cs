using BootcampTrack.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Data.Infrastructure
{
    public class BootcampTrackDataContext : DbContext
    {
        public BootcampTrackDataContext() : base("BootcampTrack")
        {
            var ensureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        
        //SQL Tables
        public IDbSet<User> Users { get; set; }
        public IDbSet<UserRole> UserRoles { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<School> Schools { get; set; }
        public IDbSet<SchoolBranch> SchoolBranches { get; set; }
        public IDbSet<Course> Courses { get; set; }
        public IDbSet<CourseInstructor> CourseInstructors { get; set; }
        public IDbSet<CourseTopic> CourseTopics { get; set; }
        public IDbSet<Topic> Topics { get; set; }
        public IDbSet<Project> Projects { get; set; }
        public IDbSet<Enrollment> Enrollments { get; set; }
        public IDbSet<Submission> Submissions { get; set; }
        public IDbSet<InstructorInvite> InstructorInvites { get; set; }
        public IDbSet<StudentInvite> StudentInvites { get; set; }
        
        //Model Relationships
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //School
            modelBuilder.Entity<School>()
                        .HasMany(s => s.SchoolBranches)
                        .WithRequired(sb => sb.School)
                        .HasForeignKey(sb => sb.SchoolAdministratorId);

            modelBuilder.Entity<School>()
                        .HasKey(s => s.SchoolAdministratorId);

            //School Branch
            modelBuilder.Entity<SchoolBranch>()
                        .HasMany(sb => sb.Instructors)
                        .WithOptional(i => i.SchoolBranch)
                        .HasForeignKey(i => i.SchoolBranchId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<SchoolBranch>()
                        .HasMany(sb => sb.InstructorInvites)
                        .WithRequired(ii => ii.SchoolBranch)
                        .HasForeignKey(ii => ii.SchoolBranchId);

            modelBuilder.Entity<SchoolBranch>()
                        .HasMany(sb => sb.Courses)
                        .WithRequired(c => c.SchoolBranch)
                        .HasForeignKey(c => c.SchoolBranchId);
            
            //Course
            modelBuilder.Entity<Course>()
                        .HasMany(c => c.CourseTopics)
                        .WithRequired(ct => ct.Course)
                        .HasForeignKey(ct => ct.CourseId);

            modelBuilder.Entity<Course>()
                        .HasMany(c => c.Enrollments)
                        .WithRequired(e => e.Course)
                        .HasForeignKey(e => e.CourseId);

            modelBuilder.Entity<Course>()
                        .HasMany(c => c.Projects)
                        .WithRequired(p => p.Course)
                        .HasForeignKey(p => p.CourseId);

            modelBuilder.Entity<Course>()
                        .HasMany(c => c.StudentInvites)
                        .WithRequired(si => si.Course)
                        .HasForeignKey(si => si.CourseId);

            modelBuilder.Entity<Course>()
                        .HasMany(c => c.CourseInstructors)
                        .WithRequired(ci => ci.Course)
                        .HasForeignKey(ci => ci.CourseId);

            //Project
            modelBuilder.Entity<Project>()
                        .HasMany(p => p.Submissions)
                        .WithRequired(s => s.Project)
                        .HasForeignKey(s => s.ProjectId);

            //Topic
            modelBuilder.Entity<Topic>()
                        .HasMany(t => t.CourseTopics)
                        .WithRequired(ct => ct.Topic)
                        .HasForeignKey(ct => ct.TopicId);

            //Role
            modelBuilder.Entity<Role>()
                        .HasMany(r => r.Users)
                        .WithRequired(ur => ur.Role)
                        .HasForeignKey(ur => ur.RoleId);
            
            //User
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Roles)
                        .WithRequired(ur => ur.User)
                        .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<User>()
                        .HasOptional(u => u.School)
                        .WithRequired(s => s.SchoolAdministrator);

            modelBuilder.Entity<User>()
                        .HasMany(u => u.CourseInstructors)
                        .WithRequired(ci => ci.Instructor)
                        .HasForeignKey(ci => ci.InstructorId);

            modelBuilder.Entity<User>()
                        .HasMany(u => u.Enrollments)
                        .WithRequired(e => e.Student)
                        .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<User>()
                        .HasMany(u => u.Submissions)
                        .WithRequired(s => s.Student)
                        .HasForeignKey(s => s.StudentId);

            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            base.OnModelCreating(modelBuilder);
        }
    }
}