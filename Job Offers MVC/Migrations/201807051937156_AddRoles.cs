namespace Job_Offers_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoleViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RoleViewModels");
        }
    }
}
