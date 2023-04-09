using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Shared.Dto;
using TeamKville.Shared.Models;

namespace TeamKville.Server.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dbContext;

        public ProductRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProductDto>> GetAll()
        {
            //mappa om products till createProductInput
            var products = await _dbContext.Products
                //.Include(x => x.Category)
                //.Include(x => x.Comments)
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Genre = x.Genre,
                    IsActive = x.IsActive,
                    Age = x.Age,
                    Rating = x.Rating
                }).ToListAsync();

            return products;
        }
        

        public async Task<ProductDto> GetById(int productId)
        {
            var product = await _dbContext.Products
                //.Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == productId);

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsActive = product.IsActive,
                Age = product.Age,
                Rating = product.Rating
            };

            return productDto;
        }

        //Skapar produkt
        public Task<string> CreateProduct(CreateProductModel createProductInput)
        {
            var newProduct = new Product
            {
                Name = createProductInput.Name,
                Description = createProductInput.Description,
                Price = createProductInput.Price,
                IsActive = createProductInput.IsActive,
                Age = createProductInput.Age,
                Rating = createProductInput.Rating
            };

            _dbContext.Products.Add(newProduct);
            _dbContext.SaveChangesAsync();

            return Task.FromResult("product created");
        }

        //Raderar produkt
        public Task<string> DeleteProduct(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);

            if (product != null)
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();

                return Task.FromResult("product deleted");
            }

            return Task.FromResult("product could not be deleted");
        }

        //Updaterar produkt
        public Task<string> UpdateProduct(UpdateProductModel updateProductInput)
        {
            var productToUpdate = _dbContext.Products.FirstOrDefault(x => x.Id == updateProductInput.Id);

            if (productToUpdate != null)
            {
                productToUpdate.Name = updateProductInput.Name;
                productToUpdate.Description = updateProductInput.Description;
                productToUpdate.Price = updateProductInput.Price;
                productToUpdate.IsActive = updateProductInput.IsActive;
                productToUpdate.Age = updateProductInput.Age;

                _dbContext.SaveChanges();
                return Task.FromResult("product updated");
            }

            return Task.FromResult("product could not be updated");
        }
    }
}
