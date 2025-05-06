using BookingService.Booking.Domain.Bookings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Booking.Infrastructure.Configurations
{
    public sealed class BookingAggregateConfiguration : IEntityTypeConfiguration<BookingAggregate>
    {
        public void Configure(EntityTypeBuilder<BookingAggregate> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id")
                .HasColumnType("bigint");
            builder.Property(x => x.Status)
                .HasColumnName("status")
                .HasColumnType("int");
            builder.Property(x => x.UserId)
                .HasColumnName("user_id")
                .HasColumnType("bigint");
            builder.Property(x => x.ResourceId)
                .HasColumnName("resource_id")
                .HasColumnType("bigint");
            builder.Property(x => x.CatalogRequestId)
                .HasColumnName("catalog_id")
                .HasColumnType("uuid");
            builder.Property(x => x.BookingDateOn)
                .HasColumnName("start_date")
                .HasColumnType("date");
            builder.Property(x => x.BookingDateOff)
                .HasColumnName("end_date")
                .HasColumnType("date");
            builder.Property(x => x.CreateBookingDate)
                .HasColumnName("created_at_date_time")
                .HasColumnType("timestamptz");
        }
    }
}
