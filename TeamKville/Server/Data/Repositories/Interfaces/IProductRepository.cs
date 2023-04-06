using Microsoft.AspNetCore.Mvc;
using TeamKville.Server.Data.DataModels;
using TeamKville.Shared.Dto;
using TeamKville.Shared.Models;

namespace TeamKville.Server.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        List<ProductDto> GetAll();
        void CreateProduct(CreateProductModel createProductInput);
        void DeleteProduct(int id);
        void UpdateProduct(UpdateProductModel updateProductInput);
        Product GetById(int productId);
    }
}
