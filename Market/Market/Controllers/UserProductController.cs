using Market.Services;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserProductController(UserProductService userProductService) : ControllerBase
{
    //TODO:     ADD PRODUCT FRO USER
    [HttpPost("add")]
    public IActionResult Add(Guid UserId, Guid ProductId)
    {
        userProductService.AddProductToUser(UserId, ProductId);
        return Ok();
    }
    
    //TODO:     GET PRODUCT FOR USER
    [HttpGet("{userId}")]
    public IActionResult GetProducts(Guid userId)
    {
        var products = userProductService.GetProductsForUser(userId);
        
        return Ok(products);
    }
    
    //TODO:     REMOVE PRODUCT FROM USER
    [HttpDelete("remove")]
    public IActionResult Remove(Guid UserId, Guid ProductId)
    {
        userProductService.RemoveProductFromUser(UserId, ProductId);
        
        return Ok("ProductFromUserDeleted");
    }
}