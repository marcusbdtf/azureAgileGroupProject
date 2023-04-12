using Microsoft.AspNetCore.Mvc;
using TeamKville.Server.Data.DataModels;
using TeamKville.Shared.Dto;
using TeamKville.Shared.Models;

namespace TeamKville.Server.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetAll();
        Task<string> CreateProduct(CreateProductModel createProductInput);
        Task<string> DeleteProduct(int id);
        Task<string> UpdateProduct(UpdateProductModel updateProductInput);
        Task<ProductDto> GetById(int productId);
        Product GetProductById(int productDtoId);
    }
}
