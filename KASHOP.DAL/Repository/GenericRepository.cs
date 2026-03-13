using KASHOP.DAL.Data;
using KASHOP.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entry)
        {
            await _context.AddAsync(entry);
            await _context.SaveChangesAsync();
            return entry;
        }

        public async Task<List<T>> GetAllAsync(string[]?includes=null)
        {
            /*Include(c => c.Translations)*/
            //return await _context.Set<T>().ToListAsync();
            IQueryable<T> query =_context.Set<T>();
            if(includes != null)
            {
                foreach(var item in includes)
                    query = query.Include(item);
            }
            return await query.ToListAsync();


        }

        public async Task<T> GetOne(Expression<Func<T, bool>> filter, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var item in includes)
                    query = query.Include(item);
            }
            return await query.FirstOrDefaultAsync(filter);

        }
    }
}
