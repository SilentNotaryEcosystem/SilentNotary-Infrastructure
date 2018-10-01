using SilentNotary.Common.Query.Criterion.Abstract;
using System.Runtime.Serialization;

namespace SilentNotary.Common.Query.Criterion
{
    [DataContract]
    public class PagingCriterion : IPagingCriterion
    {
        [DataMember(Name = "page")]
        public int Page { get; set; }
        [DataMember(Name = "count")]
        public int Count { get; set; }
    }
}
