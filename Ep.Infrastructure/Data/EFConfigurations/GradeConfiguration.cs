using EC.Infrastructure.DTO.Subjects;
using EP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EC.Infrastructure.Data.EFConfigurations
{
    public class GradeConfiguration: IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder
                .Property(x => x.SubjectName)
                .HasMaxLength(100);
        }
    }
}