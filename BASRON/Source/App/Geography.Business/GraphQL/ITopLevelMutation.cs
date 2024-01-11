using GraphQL.Types;

namespace BASRON.Business.GraphQL
{
    public interface ITopLevelMutation
    {
        void RegisterField(ObjectGraphType graphType);
    }
}