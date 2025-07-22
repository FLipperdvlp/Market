using Market.Entities;
using Market.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Market.Services;

public class UserProductService(MarketContext dbContext)
{
    //TODO:     ADD PRODUCT TO USER
    public UserProduct AddProductToUser(Guid userId, Guid productId)
    {
        var user = dbContext.Users.Find(userId);
        var product = dbContext.Products.Find(productId);

        if (user == null)
            throw new Exception("User not found in database");
        if (product == null)
            throw new Exception("Product not found in database");

        var existing = dbContext.UserProducts.Find(userId, productId);
        if (existing != null)
            throw new Exception("User already added this product to cart");


        var userProduct = new UserProduct
        {
            UserId = userId,
            ProductId = productId
        };
        
        dbContext.UserProducts.Add(userProduct);
        dbContext.SaveChanges();
        
        return userProduct;
    }
    
    //TODO:     GET PRODUCT FOR USER
    public IEnumerable<Product> GetProductsForUser(Guid userId)
    {
        return dbContext.UserProducts
            .Include(up => up.Product)
            .Where(up => up.UserId == userId)
            .Select(up => up.Product!)
            .ToList();
    }
    
    //TODO:     REMOVE PRODUCT FROM USER
    public void RemoveProductFromUser(Guid UserId, Guid productId)
    {
        var userProduct = dbContext.UserProducts
            .FirstOrDefault(up => up.UserId == UserId && up.ProductId == productId);

        if (userProduct != null)
        {
            dbContext.UserProducts.Remove(userProduct);
            dbContext.SaveChanges();
        }
    }
}