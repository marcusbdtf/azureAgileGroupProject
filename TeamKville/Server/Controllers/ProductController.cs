using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Shared.Dto;
using TeamKville.Shared.Models;

namespace TeamKville.Server.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var data = await _productRepository.GetAll();

            return Ok(data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateProduct(CreateProductModel newProduct)
        {
            await _productRepository.CreateProduct(newProduct);
            return Ok();
        }

        [HttpDelete("{productId}/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult DeleteProduct(int productId)
        {
            _productRepository.DeleteProduct(productId);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult UpdateProduct([FromBody] UpdateProductModel updateProductInput)
        {
            _productRepository.UpdateProduct(updateProductInput);
            return Ok();
        }

        [HttpGet("{productId:int}", Name = "GetProductById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductDto>> GetProductById(int productId)
        {
            var data = await _productRepository.GetById(productId);
            return Ok(data);
        }
    }
}
