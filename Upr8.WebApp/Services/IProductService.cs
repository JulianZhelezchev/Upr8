using Microsoft.AspNetCore.Http;

using System.Collections.Generic;
using System.Threading.Tasks;

using Upr8.WebApp.Models;

namespace Upr8.WebApp.Services
{
    public interface IProductService
    {
        Task CreateAsync(ProductDto product);
        Task DeleteAsync(int id);
        Task<bool> ExistAsync(int id);
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task UpdateAsync(int id, ProductDto product);
        Task<FileModel> UploadFileToFileSystem(IFormFile formfile);
    }
}