using Microsoft.EntityFrameworkCore;
using ShopP21.Entities;
using ShopP21.Persistence;

namespace ShopP21.Services;

public class CartService(ShopDbContext dbContext)
{
    /// <summary>
    /// Додати продукт до корзини
    /// </summary>
    /// <param name="productId">Айді продукта</param>
    /// <param name="userId">Айді користувача</param>
    /// <returns>Створений продукт в корзині</returns>
    /// <exception cref="Exception">Продукт/користувач не знайдений або людина вже додала продукт до корзини</exception>
    public UserProduct AddProductToUserCart(Guid productId, Guid userId)
    {
        if (dbContext.Products.Find(productId) is null)
            throw new Exception($"Product with id {productId} not found");

        if (dbContext.Users.Find(userId) is null)
            throw new Exception($"User with id {userId} not found");

        if (dbContext.UsersProducts.Find(userId, productId) is not null)
            throw new Exception($"User already added this product to cart");

        var userProduct = new UserProduct
        {
            ProductId = productId,
            UserId = userId
        };
        dbContext.UsersProducts.Add(userProduct);
        dbContext.SaveChanges();

        return userProduct;
    }

    /// <summary>
    /// Отримати продукт з корзини вказаного користувача
    /// </summary>
    /// <param name="userId">Айді користувача</param>
    /// <returns>Масив продукті</returns>
    /// <exception cref="Exception">Юзера не здайдено</exception>
    public IEnumerable<Product> GetUserProductsFromCart(Guid userId)
    {
        if (dbContext.Users.Find(userId) is null)
            throw new Exception($"User with id {userId} not found");

        return dbContext.UsersProducts
            .Include(up => up.Product)
            .Where(up => up.UserId == userId)
            .Select(up => up.Product!)
            .ToList();
    }
}