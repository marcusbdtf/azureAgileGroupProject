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

        public List<ProductDto> GetAll()
        {
            //mappa om products till createProductInput
            var products = _dbContext.Products
                //.Include(x => x.Category)
                //.Include(x => x.Comments)
                .Select(x => new ProductDto
                {
                    ProductId = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    IsActive = x.IsActive,
                    Age = x.Age,
                    Rating = x.Rating
                }).ToList();

            return products;
        }
        

        public Product GetById(int productId)
        {
            var product = _dbContext.Products
                //.Include(x => x.Category)
                .FirstOrDefault(x => x.Id == productId);

            var productInput = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsActive = product.IsActive,
                Age = product.Age,
                Rating = product.Rating
            };

            return product;
        }

        //Skapar produkt
        public void CreateProduct(CreateProductModel createProductInput)
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
            _dbContext.SaveChanges();
        }

        //Raderar produkt
        public void DeleteProduct(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }

        //Updaterar produkt
        public void UpdateProduct(UpdateProductModel updateProductInput)
        {
            var productToUpdate = _dbContext.Products.FirstOrDefault(x => x.Id == updateProductInput.Id);

            if (productToUpdate != null)
            {
                productToUpdate.Name = updateProductInput.Name;
                productToUpdate.Description = updateProductInput.Description;
                productToUpdate.Price = updateProductInput.Price;
                productToUpdate.IsActive = updateProductInput.IsActive;
                productToUpdate.Age = updateProductInput.Age;
            }

            _dbContext.SaveChanges();
        }
    }
}
