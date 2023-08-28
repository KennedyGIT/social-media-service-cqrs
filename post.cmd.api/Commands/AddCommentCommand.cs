using core.commands;

namespace post.cmd.api.Commands
{
    public class AddCommentCommand : BaseCommand
    {
        public string Comment { get; set; }

        public string Username { get; set; }
    }
}
