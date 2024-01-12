namespace BASRON.Business.Request.Models
{
    using AutoMapper;
    using BASRON.Business.BTransaction.Models;
    using BASRON.Entity.Entities;

    /// <summary>
    /// Defines the <see cref="RequestMappingProfile" />.
    /// </summary>
    public class RequestMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMappingProfile"/> class.
        /// </summary>
        public RequestMappingProfile()
        {
            CreateMap<Request, RequestReadModel>();

            CreateMap<RequestCreateModel, Request>()
                .ForMember(x => x.ReferenceNumber, opt => opt.Ignore());

            CreateMap<RequestReadModel, Request>()
               .ForMember(x => x.ReferenceNumber, opt => opt.Ignore());

            CreateMap<RequestUpdateModel, Request>()
                  .ForMember(x => x.IsActive, opt => opt.Ignore())
                  .ForMember(x => x.ReferenceNumber, opt => opt.Ignore())
                  .ForMember(x => x.Amount, opt => opt.Ignore())
                  .ForMember(x => x.CustomerId, opt => opt.Ignore())
                  .ForMember(x => x.CustomerName, opt => opt.Ignore())
                  .ForMember(x => x.TransactionDate, opt => opt.Ignore())
                  .ForMember(x => x.TransactionType, opt => opt.Ignore());
        }
    }
}
