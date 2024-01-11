namespace BASRON.Business.Request.Models
{
    using AutoMapper;
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

            CreateMap<RequestUpdateModel, Request>();
        }
    }
}
