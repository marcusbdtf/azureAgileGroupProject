using Microsoft.EntityFrameworkCore;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Shared.Dto;

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
            //mappa om products till productDto
            var products = _dbContext.Products
                //.Include(x => x.Category)
                //.Include(x => x.Comments)
                .Select(x => new ProductDto
                {
                    ProductId = x.ProductId,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    IsActive = x.IsActive,
                    Age = x.Age,
                    Rating = x.Rating
                }).ToList();

            return products;
        }
    }
}
