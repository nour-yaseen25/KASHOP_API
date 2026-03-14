using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Model;
using KASHOP.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponse> CreateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
           await _categoryRepository.CreateAsync(category);
           
            return category.Adapt<CategoryResponse>(); 
        }

        public async Task<bool> DeleteCategory(int id) 
        {
            var category=await _categoryRepository.GetOne(c=>c.Id==id);

            if (category == null) return false;
            return await _categoryRepository.DeleteAsync(category);

        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            var categories =await _categoryRepository.GetAllAsync(new string[] {nameof(Category.Translations)});
            var response = categories.Adapt<List<CategoryResponse>>();
            return response;
        }

        public async Task<CategoryResponse?> GetCategory(Expression<Func<Category, bool>> filter)
        {
            var categories = await _categoryRepository.GetOne(filter,new string[] { nameof(Category.Translations) });
            var response = categories.Adapt<CategoryResponse>();
            return response;
        }

        public async Task<CategoryResponse> UpdateCategory(int id,CategoryRequest request)
        {
            var entity = await _categoryRepository.GetOne(x => x.Id == id, new string[] { nameof(Category.Translations) });

            if (entity.Translations == null)
                entity.Translations = new List<CategoryTranslation>();

            foreach (var tr in request.Translations)
            {
                var existing = entity.Translations
                    .FirstOrDefault(x => x.Language == tr.Language);

                if (existing != null)
                {
                    tr.Adapt(existing); // update
                }
                /*
                else
                {
                    var newTranslation = tr.Adapt<CategoryTranslation>();
                    newTranslation.CategoryId = entity.Id;

                    entity.Translations.Add(newTranslation);
                }
                */
            }
            await _categoryRepository.UpdateAsync(entity);

            return entity.Adapt<CategoryResponse>();

        }
    }
}
