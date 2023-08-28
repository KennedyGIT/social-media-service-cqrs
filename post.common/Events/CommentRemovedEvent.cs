using core.events;

namespace post.common.Events
{
    public class CommentRemovedEvent : BaseEvent
    {
        public CommentRemovedEvent() : base(nameof(CommentRemovedEvent))
        {
        }

        public Guid CommentId { get; set; }
    }
}
