using Market.Entities;
using Market.Persistence;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Market.Services;

public class UserService(MarketContext dbContext)
{
    private readonly PasswordHasher<string> _hasher = new();

    
    //TODO:        GET ALL USERS
    public IEnumerable<User> GetAllUsers() 
        => dbContext.Users;
 
    
    //TODO:        GET USER BY ID
    public User GetUserById(Guid id)
        => dbContext.Users.FirstOrDefault(u => u.Id == id)
            ?? throw new Exception($"User with id {id} not found");

    
    //TODO:       ADD USER
    public User AddUser(string phone, string email, string passwordhash)
    {
        var newUser = new User
        {
            Phone = phone, 
            Email = email,
            PasswordHash = HASHPassword(passwordhash)
        };
        
        dbContext.Users.Add(newUser);
        dbContext.SaveChanges();
        
        return newUser;
    }
    
    //TODO:      UPDATE USER
    public User UpdateUser(Guid id, string phone, string email, string passwordhash)
    {
        var user = dbContext.Users.FirstOrDefault(u => u.Id == id)
            ?? throw new Exception($"User with id {id} not found");
        
        user.Phone = phone;
        user.Email = email;
        user.PasswordHash = passwordhash;
        
        dbContext.SaveChanges();
        
        return user;
    }
    
    
    //TODO:      HASHING PASSWORD FOR BETTER SECURITY BASE DATA      STATIC TYPE OF THE FUNC BC ITS VERY SECURITY AND FAST
    private string HASHPassword(string password)
    {
        return _hasher.HashPassword("user", password);
    }
    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        var result = _hasher.VerifyHashedPassword("user", hashedPassword, providedPassword);
        return result == PasswordVerificationResult.Success;
    }

    
    
    
    //TODO:      DELETE USER
    public User DeleteUser(Guid id)
    {
        var  user = dbContext.Users.FirstOrDefault(u => u.Id == id)
            ?? throw new Exception($"User with id {id} not found");
        
        dbContext.Users.Remove(user);
        dbContext.SaveChanges();
        
        return user;
    }
}