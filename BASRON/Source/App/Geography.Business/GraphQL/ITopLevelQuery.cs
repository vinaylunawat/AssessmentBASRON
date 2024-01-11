using Framework.DataAccess;
using Framework.DataAccess.Repository;
using Framework.Entity;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASRON.Business.GraphQL
{
    public interface ITopLevelQuery
    {
        void RegisterField(ObjectGraphType graphType);
    }
}
