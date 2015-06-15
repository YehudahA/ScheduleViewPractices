namespace DatabaseBindingSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Categorize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        CategoryColorString = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Appointments", "CategoryId", c => c.Int());
            CreateIndex("dbo.Appointments", "CategoryId");
            AddForeignKey("dbo.Appointments", "CategoryId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Appointments", new[] { "CategoryId" });
            DropColumn("dbo.Appointments", "CategoryId");
            DropTable("dbo.Categories");
        }
    }
}
