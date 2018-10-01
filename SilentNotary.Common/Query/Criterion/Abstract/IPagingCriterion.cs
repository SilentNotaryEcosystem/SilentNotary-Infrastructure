
namespace SilentNotary.Common.Query.Criterion.Abstract
{
    public interface IPagingCriterion : ICriterion
    {
        int Count { get; set; }
        int Page { get; set; }
    }
}