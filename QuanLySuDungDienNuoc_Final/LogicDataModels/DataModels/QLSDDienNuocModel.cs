namespace LogicDataModels.DataModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QLSDDienNuocModel : DbContext
    {
        public QLSDDienNuocModel()
            : base("name=QLSDDienNuocModel")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<QLSDDienNuocModel, Migrations.Configuration>());
        }

        public virtual DbSet<Consume> Consumes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Users_Roles> Users_Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Customer>()
                .Property(e => e.PassportID)
                .IsUnicode(false);

            modelBuilder.Entity<Price>()
                .Property(e => e.ElectricPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Price>()
                .Property(e => e.WaterPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users_Roles)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Users_Roles)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
