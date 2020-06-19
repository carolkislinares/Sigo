using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    /// <summary>
    /// Represents a category mapping configuration
    /// </summary>
    public partial class ConsolidatePaymentMap : NopEntityTypeConfiguration<ConsolidatePayment>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<ConsolidatePayment> builder)
        {
            builder.ToTable(nameof(ConsolidatePayment));
            builder.HasKey(consolidatepayment => consolidatepayment.Id);
            builder.Property(consolidatepayment => consolidatepayment.TransactionType).IsRequired();
            builder.Property(consolidatepayment => consolidatepayment.ReferenceCode).HasMaxLength(50).IsRequired();
            builder.Property(consolidatepayment => consolidatepayment.ReceiverBankId);
            builder.Property(consolidatepayment => consolidatepayment.IssuingBankId);
            builder.Property(consolidatepayment => consolidatepayment.ReceiverBank).HasMaxLength(200);
            builder.Property(consolidatepayment => consolidatepayment.IssuingBank).HasMaxLength(200);
            builder.Property(consolidatepayment => consolidatepayment.CreateOn);
            builder.Property(consolidatepayment => consolidatepayment.UpdateOn);
          

            builder.HasOne(consolidatepayment => consolidatepayment.Order)
              .WithMany()
              .HasForeignKey(consolidatepayment => consolidatepayment.OrderId)
              .IsRequired();


            base.Configure(builder);
        }

        #endregion
    }
}