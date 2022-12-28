using System.Collections.Generic;
using System.Threading.Tasks;

using Upr8.WebApp.Models;

namespace Upr8.WebApp.Services
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryDto category);
        Task DeleteAsync(int id);
        Task<List<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task UpdateAsync(int id, CategoryDto category);
        Task<bool> ExistAsync(int id);
    }
}