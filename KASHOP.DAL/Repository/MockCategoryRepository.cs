using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repository
{
    public class MockCategoryRepository : ICategoryRepository
    {
        public Task<Category> CreateAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetAllAsync(string[]? includes = null)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetOne(Expression<Func<Category, bool>> filter, string[]? includes = null)
        {
            throw new NotImplementedException();
        }
    }
}

