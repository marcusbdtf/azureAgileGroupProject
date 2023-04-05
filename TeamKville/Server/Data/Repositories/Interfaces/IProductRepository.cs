using TeamKville.Shared.Dto;

namespace TeamKville.Server.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        List<ProductDto> GetAll();
    }
}
