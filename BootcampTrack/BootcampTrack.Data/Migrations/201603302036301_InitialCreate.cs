namespace BootcampTrack.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseInstructors",
                c => new
                    {
                        CourseInstructorId = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        InstructorId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CourseInstructorId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.InstructorId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.InstructorId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        SchoolBranchId = c.Int(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        MaxEnrollments = c.Int(),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.SchoolBranches", t => t.SchoolBranchId, cascadeDelete: true)
                .Index(t => t.SchoolBranchId);
            
            CreateTable(
                "dbo.CourseTopics",
                c => new
                    {
                        CourseTopicId = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        TopicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseTopicId)
                .ForeignKey("dbo.Topics", t => t.TopicId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.TopicId);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        TopicId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TopicId);
            
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        EnrollmentId = c.Int(nullable: false, identity: true),
                        StudentId = c.String(nullable: false, maxLength: 128),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EnrollmentId)
                .ForeignKey("dbo.Users", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        SchoolBranchId = c.Int(),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        PhoneNumber = c.String(),
                        GitHubAccount = c.String(),
                        LinkedInAccount = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SchoolBranches", t => t.SchoolBranchId)
                .Index(t => t.SchoolBranchId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        SchoolAdministratorId = c.String(nullable: false, maxLength: 128),
                        SchoolName = c.String(),
                        SchoolDescription = c.String(),
                        SchoolEmailAddress = c.String(),
                        SchoolPhoneNumber = c.String(),
                        Website = c.String(),
                        ShowCoursesPublic = c.Boolean(nullable: false),
                        SchoolEstablishedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.SchoolAdministratorId)
                .ForeignKey("dbo.Users", t => t.SchoolAdministratorId)
                .Index(t => t.SchoolAdministratorId);
            
            CreateTable(
                "dbo.SchoolBranches",
                c => new
                    {
                        SchoolBranchId = c.Int(nullable: false, identity: true),
                        SchoolAdministratorId = c.String(nullable: false, maxLength: 128),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.SchoolBranchId)
                .ForeignKey("dbo.Schools", t => t.SchoolAdministratorId, cascadeDelete: true)
                .Index(t => t.SchoolAdministratorId);
            
            CreateTable(
                "dbo.InstructorInvites",
                c => new
                    {
                        InstructorInviteId = c.Int(nullable: false, identity: true),
                        SchoolBranchId = c.Int(nullable: false),
                        Token = c.String(),
                    })
                .PrimaryKey(t => t.InstructorInviteId)
                .ForeignKey("dbo.SchoolBranches", t => t.SchoolBranchId, cascadeDelete: true)
                .Index(t => t.SchoolBranchId);
            
            CreateTable(
                "dbo.Submissions",
                c => new
                    {
                        SubmissionId = c.Int(nullable: false, identity: true),
                        StudentId = c.String(nullable: false, maxLength: 128),
                        ProjectId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.SubmissionId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.StudentInvites",
                c => new
                    {
                        StudentInviteId = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        Token = c.String(),
                    })
                .PrimaryKey(t => t.StudentInviteId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentInvites", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Projects", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Enrollments", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Submissions", "StudentId", "dbo.Users");
            DropForeignKey("dbo.Submissions", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Schools", "SchoolAdministratorId", "dbo.Users");
            DropForeignKey("dbo.SchoolBranches", "SchoolAdministratorId", "dbo.Schools");
            DropForeignKey("dbo.Users", "SchoolBranchId", "dbo.SchoolBranches");
            DropForeignKey("dbo.InstructorInvites", "SchoolBranchId", "dbo.SchoolBranches");
            DropForeignKey("dbo.Courses", "SchoolBranchId", "dbo.SchoolBranches");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Enrollments", "StudentId", "dbo.Users");
            DropForeignKey("dbo.CourseInstructors", "InstructorId", "dbo.Users");
            DropForeignKey("dbo.CourseTopics", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CourseTopics", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.CourseInstructors", "CourseId", "dbo.Courses");
            DropIndex("dbo.StudentInvites", new[] { "CourseId" });
            DropIndex("dbo.Projects", new[] { "CourseId" });
            DropIndex("dbo.Submissions", new[] { "ProjectId" });
            DropIndex("dbo.Submissions", new[] { "StudentId" });
            DropIndex("dbo.InstructorInvites", new[] { "SchoolBranchId" });
            DropIndex("dbo.SchoolBranches", new[] { "SchoolAdministratorId" });
            DropIndex("dbo.Schools", new[] { "SchoolAdministratorId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "SchoolBranchId" });
            DropIndex("dbo.Enrollments", new[] { "CourseId" });
            DropIndex("dbo.Enrollments", new[] { "StudentId" });
            DropIndex("dbo.CourseTopics", new[] { "TopicId" });
            DropIndex("dbo.CourseTopics", new[] { "CourseId" });
            DropIndex("dbo.Courses", new[] { "SchoolBranchId" });
            DropIndex("dbo.CourseInstructors", new[] { "InstructorId" });
            DropIndex("dbo.CourseInstructors", new[] { "CourseId" });
            DropTable("dbo.StudentInvites");
            DropTable("dbo.Projects");
            DropTable("dbo.Submissions");
            DropTable("dbo.InstructorInvites");
            DropTable("dbo.SchoolBranches");
            DropTable("dbo.Schools");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.Enrollments");
            DropTable("dbo.Topics");
            DropTable("dbo.CourseTopics");
            DropTable("dbo.Courses");
            DropTable("dbo.CourseInstructors");
        }
    }
}
