using StudentDiaryWPF.Models.Domains;
using System.Data.Entity.ModelConfiguration;

namespace StudentDiaryWPF.Models.Configurations
{
    class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        //private int _firstNameMaxLen;
        //public int FirstNameMaxLen
        //{
        //    get { return _firstNameMaxLen; }
        //    set { _firstNameMaxLen = value;}
        //}
        public StudentConfiguration()
        {
            ToTable("dbo.Students");
            HasKey(x => x.Id);

            Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();
            Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
