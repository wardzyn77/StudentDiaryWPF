namespace StudentDiaryWPF
{
    using StudentDiaryWPF.Models.Configurations;
    using StudentDiaryWPF.Models.Domains;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext()
            : base("name=AplicationDBContext")
        {
        }
        //wskazujemy klasy Domain opisuj�ce tabele SQL
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        //Konfiguracje tabel mamy w Folderze Configurations, zatem tu musimy wskaza�, �e one s�
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());
        }
    }
}