using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopP21.Models.Cart;
using ShopP21.Services;

namespace ShopP21.Controllers;

[ApiController]
[Route("cart")]
[Authorize]
public class CartController(CartService cartService) : ControllerBase
{
    [HttpPost]
    public IActionResult AddProductToCart([FromBody] AddProductToCartRequestModel model)
    {
        // витягуємо id користувача з jwt токену
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        // додаємо товар до корзини
        cartService.AddProductToUserCart(model.ProductId, userId);

        return Ok();
    }

    [HttpGet]
    public ActionResult<IEnumerable<GetProductsFromCartResponseModel>> GetProductsFromCart()
    {
        // витягуємо id користувача з jwt токену
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        // отримуємо всі продукти з корзини
        var products = cartService.GetUserProductsFromCart(userId);

        // масив продуктів конвертуємо в масив моделей та повертаємо
        return Ok(products.Select(p => new GetProductsFromCartResponseModel(p)));
    }
}