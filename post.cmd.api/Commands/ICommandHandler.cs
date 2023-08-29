namespace post.cmd.api.Commands
{
    public interface ICommandHandler
    {
        Task HandleAsync(NewPostCommand command);
        Task HandleAsync(DeletePostCommand command);
        Task HandleAsync(EditCommentCommand command);
        Task HandleAsync(EditMessageCommand command);
        Task HandleAsync(LikePostCommand command);
        Task HandleAsync(RemoveCommentCommand command);
        Task HandleAsync(AddCommentCommand command);
    }
}
