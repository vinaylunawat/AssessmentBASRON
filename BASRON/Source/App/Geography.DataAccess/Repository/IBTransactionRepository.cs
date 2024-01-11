namespace BASRON.DataAccess.Repository
{
    using Framework.DataAccess.Repository;
    using BASRON.Entity.Entities;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="IBTransactionRepository" />.
    /// </summary>
    public interface IBTransactionRepository : IGenericRepository<BTransaction>
    {   
    }
}
