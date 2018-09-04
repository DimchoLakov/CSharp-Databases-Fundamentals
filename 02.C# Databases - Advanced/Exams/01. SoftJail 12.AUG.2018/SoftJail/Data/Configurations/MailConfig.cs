using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftJail.Data.Models;

namespace SoftJail.Data.Configurations
{
    public class MailConfig : IEntityTypeConfiguration<Mail>
    {
        public void Configure(EntityTypeBuilder<Mail> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Description)
                .IsRequired();

            builder
                .Property(x => x.Sender)
                .IsRequired();

            builder
                .Property(x => x.Address)
                .IsRequired();

            builder
                .Property(x => x.PrisonerId) //?
                .IsRequired();

            builder
                .HasOne(x => x.Prisoner)
                .WithMany(x => x.Mails)
                .HasForeignKey(x => x.PrisonerId);
        }
    }
}
