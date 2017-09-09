using System.Collections.Generic;
using PsikoterapsitlerBurada.Core.Models;

namespace PsikoterapsitlerBurada.Core.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        Category GetCategoryByCategoryId(int categoryId);
        void Add(Category category);
        void Remove(Category category);
    }
}