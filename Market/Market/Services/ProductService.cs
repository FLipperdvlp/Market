using Market.Entities;
using Market.Enums;
using Market.Persistence;

namespace Market.Services;

public class ProductService(MarketContext dbContext)
{
    public IEnumerable<Product> GetAllProducts()
        => dbContext.Products;

    
    
    //TODO:     ADD
    public Product AddProduct(string name, decimal price, int amount ,ProductCategory  category)
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

    
    
    //TODO:   UPDATE
    public Product UpdateProduct(Guid id, string name, decimal price, int amount, ProductCategory category)
    {
        var product = dbContext.Products.FirstOrDefault(p => p.Id == id && !p.IsDeleted) ?? throw new Exception($"Product with id: {id} not found");// ?? = Если слевой стороны null, то выбросить (throw) то что справа
         
        product.Name = name;
        product.Price = price;
        product.Amount = amount;
        product.Category = category;
        
        dbContext.SaveChanges();
        
        return product;
    }

    
    
    
    //TODO:     DELETE
    public void DeleteProduct(Guid id)
    {
        var product = dbContext.Products.FirstOrDefault(p => p.Id == id && !p.IsDeleted) ?? throw new Exception($"Product with id: {id} not found");
        
        product.IsDeleted = true;
        
        dbContext.SaveChanges();;
    }
    
    
    
    //TODO:      GET_PRODUCT_BY_ID
    public Product GetProductById(Guid id)
    {
        return dbContext.Products.FirstOrDefault(p => p.Id == id && !p.IsDeleted) ?? throw new Exception($"Product with id: {id} not found");
    }
}