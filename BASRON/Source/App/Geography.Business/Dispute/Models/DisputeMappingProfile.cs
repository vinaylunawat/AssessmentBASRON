namespace BASRON.Business.Dispute.Models
{
    using AutoMapper;
    using BASRON.Entity.Entities;

    /// <summary>
    /// Defines the <see cref="DisputeMappingProfile" />.
    /// </summary>
    public class DisputeMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisputeMappingProfile"/> class.
        /// </summary>
        public DisputeMappingProfile()
        {
            CreateMap<Dispute, DisputeReadModel>();

            CreateMap<DisputeCreateModel, Dispute>()
                .ForMember(x => x.ReferenceNumber, opt => opt.Ignore());

            CreateMap<DisputeUpdateModel, Dispute>();
        }
    }
}
