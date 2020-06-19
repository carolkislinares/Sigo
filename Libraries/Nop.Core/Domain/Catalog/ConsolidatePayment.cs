using Nop.Core.Domain.Orders;
using System;

namespace Nop.Core.Domain.Catalog
{
    /// <summary>
    /// Represents a ConsolidatePayment
    /// </summary>
    public partial class ConsolidatePayment : BaseEntity
    {

        /// <summary>
        /// Gets or sets the ReferenceCode 
        /// </summary>
        public string ReferenceCode { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public virtual Order Order { get; set; }

        /// <summary>
        /// Gets or sets a value of used category template identifier
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets a value StoreId
        /// </summary>
        public int StoreId { get; set; }
        /// <summary>
        /// Get or set a value of used bank receiver
        /// </summary>
        public int ReceiverBankId { get; set; }

        /// <summary>
        /// Get or set a value of used bank issuing
        /// </summary>
        public int IssuingBankId { get; set; }

        /// <summary>
        /// Get or set a value ReceiverBank
        /// </summary>
        public string ReceiverBank { get; set; }

        /// <summary>
        /// Get or set a value IssuingBank
        /// </summary>
        public string IssuingBank { get; set; }

        /// <summary>
        /// Get or set a value CreateOn
        /// </summary>
        public DateTime CreateOn { get; set; }

        /// <summary>
        /// Get or set a value UpdateOn
        /// </summary>
        public DateTime UpdateOn { get; set; }

        /// <summary>
        /// Get or set a value TransactionType
        /// </summary>
        public int TransactionType { get; set; }
    }
}