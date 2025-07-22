using Market.Entities;
using Market.Persistence;

namespace Market.Services;

public class UserProductService(MarketContext dbContext)
{
    //TODO:     ADD PRODUCT TO USER
    public void AddProductToUser(Guid UserId, Guid productId)
    {
        var user = dbContext.UserProducts.Find(UserId);
        var product = dbContext.Products.Find(productId);
        
        if(user == null || product == null) throw new Exception("User or Product not found");
        
        var userProduct = new UserProduct
        {
            UserId = UserId,
            ProductId = productId
        };
        
        dbContext.UserProducts.Add(userProduct);
        dbContext.SaveChanges();
    }
    
    //TODO:     GET PRODUCT FOR USER
    public List<Product> GetProductsForUser(Guid userId)
    {
        return dbContext.UserProducts
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