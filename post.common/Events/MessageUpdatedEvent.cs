using core.events;

namespace post.common.Events
{
    public class MessageUpdatedEvent : BaseEvent
    {
        public MessageUpdatedEvent() : base(nameof(MessageUpdatedEvent)) 
        {

        }

        public string Message { get; set; }
    }
}
