namespace LogicDataModels.Migrations
{
    using LogicDataModels.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LogicDataModels.DataModels.QLSDDienNuocModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LogicDataModels.DataModels.QLSDDienNuocModel context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            QLSDDienNuocModel db = new QLSDDienNuocModel();
            if (!db.Users.Any(x => x.UserName == "admin"))
            {
                db.Users.Add(new User { UserName = "admin", Password = Commons.En_Decrypt.Encrypt("123a@"), isAdmin = 1, isDelete = false, DisplayName = "Administrator" });
            }
            List<string> RoleName = new List<string>
            {
                "Quản lý khách hàng",
                "Quản lý nhân viên",
                "Quản lý đơn giá",
                "Quản lý tiêu thụ"
            };
            if (!db.Roles.Any())
            {
                foreach (var item in RoleName)
                {
                    db.Roles.Add(new Role { RoleName = item, isDelete = false });
                }
            }
            db.SaveChanges();
        }
    }
}
