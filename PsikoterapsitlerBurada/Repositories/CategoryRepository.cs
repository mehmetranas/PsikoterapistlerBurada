using PsikoterapsitlerBurada.Models;
using System.Collections.Generic;
using System.Linq;

namespace PsikoterapsitlerBurada.Repositories
{
    public class CategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryByCategoryId(int categoryId)
        {
            return _context.Categories.SingleOrDefault(c => c.Id ==categoryId);
        }
    }
}