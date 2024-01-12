namespace BASRON.Business.BTransaction.Models
{
    using AutoMapper;
    using BASRON.Entity.Entities;

    /// <summary>
    /// Defines the <see cref="BTransactionMappingProfile" />.
    /// </summary>
    public class BTransactionMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BTransactionMappingProfile"/> class.
        /// </summary>
        public BTransactionMappingProfile()
        {
            CreateMap<BTransaction, BTransactionReadModel>();

            CreateMap<BTransactionCreateModel, BTransaction>()
                .ForMember(x => x.ReferenceNumber, opt => opt.Ignore());

            CreateMap<BTransactionReadModel, BTransaction>()
                .ForMember(x => x.ReferenceNumber, opt => opt.Ignore());

            CreateMap<BTransactionUpdateModel, BTransaction>();
        }
    }
}
