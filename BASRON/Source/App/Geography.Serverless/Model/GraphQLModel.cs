using Newtonsoft.Json.Linq;

namespace BASRON.Serverless.Model
{
    public class GraphQLModel
    {
        public string Query { get; set; }
        public Dictionary<string, object>? Variables { get; set; }
    }
}
