namespace MVC5_full_version.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stru1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisterViewModels", "ImagePath", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserImagePath");
            DropColumn("dbo.RegisterViewModels", "ImagePath");
        }
    }
}
