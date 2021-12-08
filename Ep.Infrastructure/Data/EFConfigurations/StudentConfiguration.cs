using EP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EC.Infrastructure.Data.EFConfigurations
{
    public class StudentConfiguration: IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
                .Property(x => x.Name)
                .HasMaxLength(25)
                .IsRequired();

            builder
                .Property(x => x.Surname)
                .HasMaxLength(50)
                .IsRequired();

            builder.Ignore(x => x.FullName);

            builder
                .Property(x => x.GroupNumber)
                .HasMaxLength(10)
                .IsRequired();

            builder
                .HasMany(x => x.Grades)
                .WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId);
        }
    }
}