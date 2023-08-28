using core.commands;

namespace post.cmd.api.Commands
{
    public class RemoveCommentCommand : BaseCommand
    {
        public Guid CommentId { get; set; }

        public string Username { get; set; }
    }
}
