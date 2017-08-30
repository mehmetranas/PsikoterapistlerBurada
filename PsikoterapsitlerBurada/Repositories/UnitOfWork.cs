using PsikoterapsitlerBurada.Models;

namespace PsikoterapsitlerBurada.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IAnswerRepository Answers { get; private set; }
        public IUserRepository Users { get; private set; }
        public IUserNotificationRepository UserNotifications { get; private set; }
        public IVoteRepository Votes { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public INotificationRepository Notifications { get; private set; }
        public IFollowingRepository Followings { get; private set; }
        public IQuestionRepository Questions { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Answers = new AnswerRepository(_context);
            Users = new UserRepository(_context);
            UserNotifications = new UserNotificationRepository(_context);
            Votes = new VoteRepository(_context);
            Categories = new CategoryRepository(_context);
            Notifications = new NotificationRepository(_context);
            Followings = new FollowingRepository(_context);
            Questions = new QuestionRepository(_context);
        }

        public void Complate()
        {
            _context.SaveChanges();
        }
    }
}