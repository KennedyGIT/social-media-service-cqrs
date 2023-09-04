using core.Queries;

namespace post.query.api.Queries
{
    public class FindPostByIdQuery : BaseQuery
    {
        public Guid id { get; set; }    
    }
}
