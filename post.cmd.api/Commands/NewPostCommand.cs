using core.commands;

namespace post.cmd.api.Commands
{
    public class NewPostCommand : BaseCommand
    {
        public string Author { get; set; }
        public string Message { get; set; }
    }
}
