namespace PsikoterapsitlerBurada.Core.Repositories
{
    public interface IUnitOfWork
    {
        IAnswerRepository Answers { get; }
        ICategoryRepository Categories { get; }
        IFollowingRepository Followings { get; }
        INotificationRepository Notifications { get; }
        IQuestionRepository Questions { get; }
        IUserNotificationRepository UserNotifications { get; }
        IUserRepository Users { get; }
        IVoteRepository Votes { get; }

        void Complete();
    }
}