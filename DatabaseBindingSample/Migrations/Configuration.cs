namespace DatabaseBindingSample.Migrations
{
    using DatabaseBindingSample.Models;
    using System;
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
                category => category.DisplayName,
                new CategoryModel { DisplayName = "Red category", CategoryColor = System.Windows.Media.Colors.Red },
                new CategoryModel { DisplayName = "Yellow category", CategoryColor = System.Windows.Media.Colors.Yellow },
                new CategoryModel { DisplayName = "Gray category", CategoryColor = System.Windows.Media.Color.FromRgb(200, 200, 200) }
            );
        }
    }
}
