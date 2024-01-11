using System;

namespace BASRON.Business.Request.Models
{

    /// <summary>
    /// Defines the <see cref="RequestUpdateModel" />.
    /// </summary>
    public class RequestUpdateModel //: RequestReadModel
    {
        public Guid ReferenceNumber { get; set; }
        public string CustomerId { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
    }
}
