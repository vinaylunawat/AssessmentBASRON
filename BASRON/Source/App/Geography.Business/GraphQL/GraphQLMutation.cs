using GraphQL.Types;
using System.Collections.Generic;

namespace BASRON.Business.GraphQL
{
    public class GraphQLMutation : ObjectGraphType       
    {
        public GraphQLMutation(IEnumerable<ITopLevelMutation> topLevelMutations)
        {
            foreach (var mutation in topLevelMutations)
            {
                mutation.RegisterField(this);
            }
        }
    }
}
