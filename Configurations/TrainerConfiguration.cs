using FitnessSimulation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace FitnessSimulation.Configurations
{
    public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(256);
            builder.ToTable(opt =>
            {
                opt.HasCheckConstraint("CK_Trainers_Experience", "[Experience]>3");
            });
            builder.Property(x=>x.ImagePath).IsRequired().HasMaxLength(1024);
            builder.HasOne(x=>x.Category).WithMany(x=>x.Trainers).HasForeignKey(x=>x.CategoryId).HasPrincipalKey(x=>x.Id).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
