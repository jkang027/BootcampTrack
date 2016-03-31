namespace BootcampTrack.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailToInvites : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InstructorInvites", "EmailAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InstructorInvites", "EmailAddress");
        }
    }
}
