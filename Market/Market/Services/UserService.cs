using Market.Entities;
using Market.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Market.Services;

public class UserService(MarketContext dbContext)
{
    private readonly PasswordHasher<User> _hasher = new();
    
    //TODO:        GET ALL USERS
    public IEnumerable<User> GetAllUsers() 
        => dbContext.Users;
    
    //TODO:        GET USER BY ID
    public User GetUserById(Guid id)
        => dbContext.Users.FirstOrDefault(u => u.Id == id)
            ?? throw new Exception($"User with id {id} not found");
    
    //TODO:       ADD USER
    public User AddUser(string phone, string email, string password)
    {
        if (!IsValidPassword(password))
            throw new Exception("Password does not meet the security requirements");
        
        if (dbContext.Users.Any(u => u.Phone == phone))
            throw new Exception($"User with phone: {phone} already exists");
        
        if(dbContext.Users.Any(u => u.Email == email)) 
            throw new Exception($"User with email: {email} already exists");
        
        var newUser = new User
        {
            Phone = phone, 
            Email = email,
            PasswordHash = "",
        };
        newUser.PasswordHash = HASHPassword(newUser, password);
        
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
    
    //TODO:      HASHING PASSWORD FOR BETTER SECURITY BASE DATA
    private string HASHPassword(User user, string password)
    {
        return _hasher.HashPassword(user, password);
    }
    public bool VerifyPassword(User user, string hashedPassword, string providedPassword)
    {
        var result = _hasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
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
    
    //TODO:     VALIDATION VERIFICATION
    public bool IsValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))// Spaces
            return false;
        if (password.Length < 8)// < 8
            return false;
        if (!password.Any(char.IsDigit))// 0 - 9
            return false;
        if (!password.Any(char.IsUpper))// A - Z
            return false;
        return true;
    }
}