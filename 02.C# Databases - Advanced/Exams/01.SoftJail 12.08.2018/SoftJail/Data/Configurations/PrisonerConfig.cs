using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftJail.Data.Models;

namespace SoftJail.Data.Configurations
{
    public class PrisonerConfig : IEntityTypeConfiguration<Prisoner>
    {
        public void Configure(EntityTypeBuilder<Prisoner> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.FullName)
                .IsRequired();

            builder
                .Property(x => x.Nickname)
                .IsRequired();

            builder
                .Property(x => x.Age)
                .IsRequired();

            builder
                .Property(x => x.IncarcerationDate)
                .IsRequired();

            builder
                .Property(x => x.Bail)
                .IsRequired(false);

            builder
                .Property(x => x.CellId)
                .IsRequired(false);

            builder
                .Property(x => x.ReleaseDate)
                .IsRequired(false);
        }
    }
}
