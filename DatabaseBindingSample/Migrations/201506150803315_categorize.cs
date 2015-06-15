namespace DatabaseBindingSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categorize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        CategoryColorString = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Appointments", "CategoryId", c => c.Int());
            CreateIndex("dbo.Appointments", "CategoryId");
            AddForeignKey("dbo.Appointments", "CategoryId", "dbo.CategoryModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "CategoryId", "dbo.CategoryModels");
            DropIndex("dbo.Appointments", new[] { "CategoryId" });
            DropColumn("dbo.Appointments", "CategoryId");
            DropTable("dbo.CategoryModels");
        }
    }
}
