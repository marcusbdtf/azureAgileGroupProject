using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Shared.Dto;
using TeamKville.Shared.Models;

namespace TeamKville.Server.Controllers
{
    //[Route["api/[controller]")]
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        //Hämta alla produkter
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var data = await _productRepository.GetAll();

            return Ok(data);
        }

        //Skapar produkt
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateProduct(CreateProductModel newProduct)
        {
            await _productRepository.CreateProduct(newProduct);
            return Ok();
        }

        //Raderar produkt baserat på produktId
        [HttpDelete("{productId}/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult DeleteProduct(int productId)
        {
            _productRepository.DeleteProduct(productId);
            return Ok();
        }

        //Updaterar produkt baserat på id
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult UpdateProduct([FromBody] UpdateProductModel updateProductInput)
        {
            _productRepository.UpdateProduct(updateProductInput);
            return Ok();
        }

        //Hämtar en updateProductInput baserat på Id
        [HttpGet("{productId:int}", Name = "GetProductById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ProductDto> GetProductById(int productId)
        {
            var data = _productRepository.GetById(productId);
            return Ok(data);
        }
    }
}
