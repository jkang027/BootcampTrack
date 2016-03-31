namespace BootcampTrack.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailToStudentInvites : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentInvites", "EmailAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentInvites", "EmailAddress");
        }
    }
}
