using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Upr8.WebApp.Data.Models;
using Upr8.WebApp.Data;
using Upr8.WebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Diagnostics;

namespace Upr8.WebApp.Services
{
    public class ProductService : IProductService
    {
        private readonly EFContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public ProductService(EFContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var result = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            return _mapper.Map<List<ProductDto>>(result);
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(c => c.Id == id);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task CreateAsync(ProductDto product)
        {
            var productToAdd = _mapper.Map<Products>(product);
            if (product.ImageFile != null)
            {
                using MemoryStream ms = new MemoryStream();
                await product.ImageFile.CopyToAsync(ms);
                productToAdd.ImageContent = ms.ToArray();
                productToAdd.ImageMimeType = product.ImageFile.ContentType;
            }
            _context.Products.Add(productToAdd);
            await _context.SaveChangesAsync();
            product.Id = productToAdd.Id;
        }

        public async Task UpdateAsync(int id, ProductDto product)
        {
            var productToEdit = _mapper.Map<Products>(product);

            _context.Attach(new Products() { Id = id })
                .CurrentValues.SetValues(productToEdit);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var productToDelete = await _context.Products
                .FirstOrDefaultAsync(c => c.Id == id);
            _context.Remove(productToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Products.AnyAsync(c => c.Id == id);
        }

        public async Task<FileModel> UploadFileToFileSystem(IFormFile formfile)
        {
            var result = new FileModel();
            if (formfile == null)
            {
                return result;
            }
            result.OriginalFileName = formfile.FileName;
            result.FileName = Path.GetRandomFileName() + Path.GetExtension(formfile.FileName);
            result.DatabaseValue = Path.Combine("Uploads", result.FileName);
            result.WwwRootPath = Path.Combine(_environment.WebRootPath, result.DatabaseValue);

            //Create upload directory if not exists
            var uploadsDir = Path.Combine(_environment.WebRootPath, "Uploads");
            if (!Directory.Exists(uploadsDir))
            {
                Directory.CreateDirectory(uploadsDir);
            }

            using FileStream fs = new FileStream(result.WwwRootPath, FileMode.Create, FileAccess.Write);
            await formfile.CopyToAsync(fs);

            return result;
        }
    }
}
