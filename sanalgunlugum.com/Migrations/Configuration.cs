namespace sanalgunlugum.com.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<sanalgunlugum.com.Models.Manager.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(sanalgunlugum.com.Models.Manager.DatabaseContext context)
        {

        }
    }
}
