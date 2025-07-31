using ShopP21.Entities;
using ShopP21.Enums;
using ShopP21.Persistence;

namespace ShopP21.Services;

public class ProductsService(ShopDbContext dbContext)
{
    /// <summary>
    /// Отримати всі продукти
    /// </summary>
    /// <returns>Масив продуктів</returns>
    public IEnumerable<Product> GetAllProducts()
        => dbContext.Products;

    /// <summary>
    /// Отримати продукт по id
    /// </summary>
    /// <param name="id">ID продукту</param>
    /// <returns>Продукт</returns>
    /// <exception cref="Exception">Продукт не знайдено</exception>
    public Product GetProductById(Guid id)
    {
        var product = dbContext.Products.Find(id);

        if (product is null)
            throw new Exception("Product not found");

        return product;
    }

    /// <summary>
    /// Створити новий продукт
    /// </summary>
    /// <param name="name">Назва</param>
    /// <param name="price">Ціна</param>
    /// <param name="amount">Кількість на складі</param>
    /// <param name="category">Категорія продукта</param>
    /// <returns>Створений ініціалізований продукт</returns>
    public Product AddProduct(string name, decimal price, int amount, ProductCategory category)
    {
        var newProduct = new Product
        {
            Name = name,
            Price = price,
            Amount = amount,
            Category = category
        };

        dbContext.Products.Add(newProduct);
        dbContext.SaveChanges();

        return newProduct;
    }

    /// <summary>
    /// Видалити продукт
    /// </summary>
    /// <param name="id">Id продукта</param>
    /// <exception cref="Exception">Продукт не знайдено</exception>
    public void DeleteProduct(Guid id)
    {
        var product = dbContext.Products.Find(id);

        if (product is null)
            throw new Exception("Product not found");

        product.IsDeleted = true;
        dbContext.SaveChanges();
    }

    /// <summary>
    /// Оновити продукт
    /// </summary>
    /// <param name="id">Айді продукта</param>
    /// <param name="name">Назва</param>
    /// <param name="price">Ціна</param>
    /// <param name="amount">Кількість</param>
    /// <param name="category">Категорія</param>
    /// <returns>Оновлений продукт</returns>
    /// <exception cref="Exception">Продукт не здайено</exception>
    public Product UpdateProduct(Guid id, string name, decimal price, int amount, ProductCategory category)
    {
        var product = dbContext.Products.Find(id);

        if (product is null)
            throw new Exception("Product not found");

        product.Name = name;
        product.Price = price;
        product.Amount = amount;
        product.Category = category;

        dbContext.SaveChanges();

        return product;
    }
}