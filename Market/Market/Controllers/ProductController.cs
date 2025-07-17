using Market.Models.Products;
using Market.Services;
using Microsoft.AspNetCore.Mvc;
using Market.Controllers;
using Market.Enums;

namespace Market.Controllers;

[ApiController]
[Route("products")]
public class ProductController(ProductService productService) : ControllerBase
{
    //TODO:    GET ALL
    [HttpGet]
    public ActionResult<IEnumerable<GetAllProductsResponseModel>> GetAllProducts()
    {
        //TODO: GET ALL PRODUCTS
        var products = productService.GetAllProducts();
        
        var models = products.Select(products =>
            new GetAllProductsResponseModel(products));
        
        return Ok(models);
    }
    
    //TODO:    GET BY ID
    [HttpGet("{productId}")]
    public IActionResult GetProductById([FromRoute] Guid productid)
    {
        var product = productService.GetProductById(productid);
        
        return Ok(new GetProductByIdResponseModel(product));
    }
    
    //TODO:    CREATE PRODUCT
    [HttpPost]
    public ActionResult<CreateProductResponseModel> CreateProduct([FromBody] CreateProductRequestModel model)
    {
        var product = productService.AddProduct(
                model.Name,
                model.Price,
                model.Amount,
                model.Category
            );
        return Ok(new CreateProductResponseModel(product));
    }
    
    //TODO:    UPDATE PRODUCT
    [HttpPut("{productId:guid}")]
    public ActionResult<UpdateProductResponseModel> UpdateProduct([FromBody] Guid productId, [FromBody] UpdateProductRequestModel model)
    {
        var updatedProduct = productService.UpdateProduct(
            productId,
            model.Name,
            model.Price,
            model.Amount,
            model.Category
        );        
        return Ok(new UpdateProductResponseModel(updatedProduct));
    }
    
    //TODO:    DELETE PRODUCT
    [HttpDelete("{productId:guid}")]
    public ActionResult DeleteProduct([FromQuery] Guid productId)
    {
        var delete = productService.DeleteProduct(productId);
        return NoContent();
    }
}
