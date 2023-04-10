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
            //mappa om products till DTO
            var products = await _dbContext.Products
	            .Include(x => x.Genre)
				.Include(x => x.Category)
                .Include(x => x.Comments)
                .Select(x => new ProductDto
                { 
                    Id = x.ProductId,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Category = new CategoryDto
                    {
                        CategoryId = x.Category.CategoryId,
                        Name = x.Category.Name
                    },
                    Genre = new GenreDto
                    {
                        GenreId = x.Genre.GenreId,
                        Name = x.Genre.Name
                    },
                    IsActive = x.IsActive,
                    Age = x.Age,
                    Comments = x.Comments.Select(x => new CommentDto
                    {
                        CommentId = x.CommentId,
                        Name = x.Name,
                        Text = x.Text,
                        Rating = x.Rating,
                        Date = x.Date
                    })

                }).ToListAsync();

            return products;
        }
        

        public async Task<ProductDto> GetById(int productId)
        {
            var product = await _dbContext.Products
	            .Include(x => x.Genre)
                .Include(x => x.Category)
                .Include(x => x.Comments)

                .FirstOrDefaultAsync(x => x.ProductId == productId);

            var productDto = new ProductDto
            {
                Id = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsActive = product.IsActive,
                Age = product.Age,
                Category = new CategoryDto
                {
                    CategoryId = product.Category.CategoryId,
                    Name = product.Category.Name,
                },
                Genre = new GenreDto
                {
                    GenreId = product.Genre.GenreId,
                    Name = product.Genre.Name
                },
                Comments = product.Comments.Select(x => new CommentDto
                {
	                CommentId = x.CommentId,
	                Name = x.Name,
	                Text = x.Text,
	                Rating = x.Rating,
	                Date = x.Date
                }).ToList()
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
                CategoryId = createProductInput.CategoryId,
                GenreId = createProductInput.GenreId,
                Comments = null
            };

            _dbContext.Products.Add(newProduct);
            _dbContext.SaveChanges();

            return Task.FromResult("Product successfully created");
        }

        //Raderar produkt
        public Task<string> DeleteProduct(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.ProductId == id);

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
            var productToUpdate = _dbContext.Products.FirstOrDefault(x => x.ProductId == updateProductInput.Id);

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
