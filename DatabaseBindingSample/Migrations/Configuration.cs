namespace DatabaseBindingSample.Migrations
{
    using DatabaseBindingSample.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SchedulingDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SchedulingDbContext context)
        {
            context.Categories.AddOrUpdate(
                category => category.CategoryName,
                new CategoryModel { CategoryName = "Red category", CategoryColor = System.Windows.Media.Colors.Red },
                new CategoryModel { CategoryName = "Yellow category", CategoryColor = System.Windows.Media.Colors.Yellow },
                new CategoryModel { CategoryName = "Gray category", CategoryColor = System.Windows.Media.Color.FromRgb(200, 200, 200) }
            );

            context.ResourceTypes.AddOrUpdate(
                type => type.Name,
                new ResourceTypeModel()
                {
                    Name = "Room",
                    Resources = new List<ResourceModel>
                    {
                        new ResourceModel(){ResourceName = "Room1"},
                        new ResourceModel(){ResourceName = "Room2"}
                    }
                },
                new ResourceTypeModel()
                {
                    Name = "Agent",
                    Resources = new List<ResourceModel>
                    {
                        new ResourceModel(){ResourceName = "John"},
                        new ResourceModel(){ResourceName = "Scott"}
                    }
                }
                );
        }
    }
}
