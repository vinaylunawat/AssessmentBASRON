using System;

namespace BASRON.Business.Dispute.Models
{

    /// <summary>
    /// Defines the <see cref="DisputeUpdateModel" />.
    /// </summary>
    public class DisputeUpdateModel //: DisputeReadModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
    }
}
