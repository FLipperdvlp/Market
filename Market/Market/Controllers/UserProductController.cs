using System.Security.Claims;
using Market.Models.Cart;
using Market.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;


[ApiController]
[Route("cart")]
[Authorize]
public class UserProductController(UserProductService userProductService) : ControllerBase
{
    //TODO:     ADD PRODUCT FRO USER
    [HttpPost]
    public IActionResult AddProductToCard([FromBody]  AddProductToCartRequestModel model)
    {
        //TODO: GET USER ID FROM JWT TOKEN
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        //TODO: ADD PRODUCT INTO THE CART
        userProductService.AddProductToUser(model.ProductId, userId);
        
        return Ok();
    }
    
    //TODO:     GET PRODUCT FOR USER
    [HttpGet]
    public ActionResult<IEnumerable<GetProductsFromCartResponseModel>> GetProductsFromCart()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var products = userProductService.GetProductsForUser(userId);
        
        return Ok(products.Select(p => new GetProductsFromCartResponseModel(p)));
    }
    
    //TODO:     REMOVE PRODUCT FROM USER
    [HttpDelete("remove")]
    public IActionResult Remove(Guid UserId, Guid ProductId)
    {
        userProductService.RemoveProductFromUser(UserId, ProductId);
        
        return Ok("ProductFromUserDeleted");
    }
}