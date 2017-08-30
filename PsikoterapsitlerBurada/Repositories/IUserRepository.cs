using System.Collections.Generic;
using PsikoterapsitlerBurada.Models;

namespace PsikoterapsitlerBurada.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetAllUsers();
        ApplicationUser GetUserById(string userId);
        ApplicationUser GetUsers(string id);
        List<ApplicationUser> GetUsersByQuery(string query);
        IEnumerable<ApplicationUser> GetAllUsersWithOutAuthUser(string userId);
        IEnumerable<int> GetUserLikeAnswersIdByQuestionId(ApplicationUser user, int questionId);
        ICollection<Question> GetUserFavoriteQuestions(string userId);
        void Add(ApplicationUser user);
        void Remove(ApplicationUser user);
    }
}