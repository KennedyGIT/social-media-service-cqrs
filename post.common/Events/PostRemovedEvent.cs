using core.events;

namespace post.common.Events
{
    public class PostRemovedEvent : BaseEvent
    {
        public PostRemovedEvent() : base(nameof(PostRemovedEvent))
        {
        }

        public Guid CommentId { get; set; }
    }
}
