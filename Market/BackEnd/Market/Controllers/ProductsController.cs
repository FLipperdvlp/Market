using Microsoft.AspNetCore.Mvc;
using ShopP21.Models.Products;
using ShopP21.Services;

namespace ShopP21.Controllers;

[ApiController]
[Route("products")]
public class ProductsController(ProductsService productsService) : ControllerBase
{
    // Отримати всі продукти
    [HttpGet]
    public ActionResult<IEnumerable<GetAllProductsResponseModel>> GetAllProducts()
    {
        // Отримуємо всі продукти
        var products = productsService.GetAllProducts();

        // Конвертуємо масив продуктів в масив моделей
        var models = products.Select(product =>
            new GetAllProductsResponseModel(product));

        return Ok(models);
    }

    // Отримати продукт по id
    [HttpGet("{productId:guid}")]
    public ActionResult<GetProductByIdResponseModel> GetProductById([FromRoute] Guid productId)
    {
        // Отримуємо продукт по id
        var product = productsService.GetProductById(productId);

        // Конвертуємо отриманий продукт в модель та повертаємо його
        return Ok(new GetProductByIdResponseModel(product));
    }

    // Створити продукт
    [HttpPost]
    public ActionResult<CreateProductResponseModel> CreateProduct([FromBody] CreateProductRequestModel model)
    {
        var createdProduct = productsService.AddProduct(model.Name, model.Price, model.Amount, model.Category);
        return Ok(new CreateProductResponseModel(createdProduct));
    }
    
    // Оновити продукт
    [HttpPut("{productId:guid}")]
    public ActionResult<UpdateProductResponseModel> UpdateProduct([FromRoute] Guid productId, [FromBody] UpdateProductRequestModel model)
    {
        var updatedProduct =
            productsService.UpdateProduct(productId, model.Name, model.Price, model.Amount, model.Category);
        return Ok(new UpdateProductResponseModel(updatedProduct));
    }
    
    // Видалити продукт
    [HttpDelete("{productId:guid}")]
    public IActionResult DeleteProduct([FromRoute] Guid productId)
    {
        productsService.DeleteProduct(productId);
        return Ok();
    }
}