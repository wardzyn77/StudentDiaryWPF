using StudentDiaryWPF.Models.Domains;
using System.Data.Entity.ModelConfiguration;

namespace StudentDiaryWPF.Models.Configurations
{
    class RatingConfiguration : EntityTypeConfiguration<Rating>
    {
        public RatingConfiguration()
        {
            ToTable("dbo.Ratings");
            HasKey(x => x.Id);
        }
    }
}
