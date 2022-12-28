using AutoMapper;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

using Upr8.WebApp.Data;
using Upr8.WebApp.Data.Models;
using Upr8.WebApp.Models;

namespace Upr8.WebApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly EFContext _context;
        private readonly IMapper _mapper;

        public CategoryService(EFContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var result = await _context.Categories.ToListAsync();

            return _mapper.Map<List<CategoryDto>>(result);
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task CreateAsync(CategoryDto category)
        {
            var categoryToAdd = _mapper.Map<Categories>(category);
            _context.Categories.Add(categoryToAdd);
            await _context.SaveChangesAsync();
            category.Id = categoryToAdd.Id;
        }

        public async Task UpdateAsync(int id, CategoryDto category)
        {
            var categoryToEdit = _mapper.Map<Categories>(category);

            _context.Attach(new Categories() { Id = id })
                .CurrentValues.SetValues(categoryToEdit);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var categoryToDelete = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
            _context.Remove(categoryToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }
    }
}
