using System.Collections.Generic;
using PsikoterapsitlerBurada.Models;

namespace PsikoterapsitlerBurada.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        Category GetCategoryByCategoryId(int categoryId);
        void Add(Category category);
        void Remove(Category category);
    }
}