using post.common.DTOs;
using post.query.domain.Entities;

namespace post.query.api.DTOs
{
    public class PostLookupResponse : BaseResponse
    {
        public List<PostEntity> Posts { get; set; }
    }
}
