using GymManagement.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Common.Persistence;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.SubscriptionType)
            .ValueGeneratedNever();

        builder.Property("_adminId")
            .HasColumnName("AdminId");
        
        builder.Property(s => s.SubscriptionType)
            .HasConversion(
                type => type.ToString(),
                value => Enum.Parse<SubscriptionType>(value));
    }
}