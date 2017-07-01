namespace DemoSinai.Models
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<School> Schools { get; set; }

        public DbSet<Department> Departments { get; set; }

        public System.Data.Entity.DbSet<DemoSinai.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<DemoSinai.Models.Student> Students { get; set; }
    }
}