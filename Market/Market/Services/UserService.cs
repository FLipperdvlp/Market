using System.Security.Cryptography;
using System.Text;
using Market.Entities;
using Market.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Market.Services;

public class UserService(MarketContext dbContext)
{
    //TODO:        GET USER BY ID
    public User GetUserById(Guid userId)
        => dbContext.Users.Find(userId)
            ?? throw new Exception("User not found");
    
    //TODO:        GET USER BY CREDENTIALS
    public User GetUserByCredentials(string phoneOrEmail, string password)
    {
        var user = dbContext.Users.FirstOrDefault(u => u.Phone == phoneOrEmail || u.Email == phoneOrEmail);

        if (user is null || user.PasswordHash != HASHPassword(password))
            throw new Exception("User with given phone or email not found");

        return user;
    }

    
    
    //TODO:       ADD USER
    public User AddUser(string phone, string email, string password)
    {
        //if (!IsValidPassword(password))
            //throw new Exception("Password does not meet the security requirements");
        
        if (dbContext.Users.Any(u => u.Phone == phone))
            throw new Exception($"User with phone: {phone} already exists");
        
        if(dbContext.Users.Any(u => u.Email == email)) 
            throw new Exception($"User with email: {email} already exists");

        var newUser = new User
        {
            Email = email,
            Phone = phone,
            PasswordHash = HASHPassword(password),
            Role = UserRole.User
        };

        dbContext.Users.Add(newUser);
        dbContext.SaveChanges();

        return newUser;
    }
    
    
    //TODO:      HASHING PASSWORD FOR BETTER SECURITY BASE DATA
    private string HASHPassword(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(bytes);
        
        var builder = new StringBuilder();
        
        foreach (var b in hashBytes)
            builder.Append(b.ToString("x2"));
        
        return builder.ToString();
    }
    // //TODO:      UPDATE USER
    // public User UpdateUser(Guid id, string phone, string email, string passwordhash)
    // {
    //     var user = dbContext.Users.FirstOrDefault(u => u.Id == id)
    //         ?? throw new Exception($"User with id {id} not found");
    //     
    //     user.Phone = phone;
    //     user.Email = email;
    //     user.PasswordHash = passwordhash;
    //     
    //     dbContext.SaveChanges();
    //     
    //     return user;
    // }
    // public bool VerifyPassword(User user, string hashedPassword, string providedPassword)
    // {
    //     var result = _hasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
    //     return result == PasswordVerificationResult.Success;
    // }
    // //TODO:      DELETE USER
    // public User DeleteUser(Guid id)
    // {
    //     var  user = dbContext.Users.FirstOrDefault(u => u.Id == id)
    //         ?? throw new Exception($"User with id {id} not found");
    //     
    //     dbContext.Users.Remove(user);
    //     dbContext.SaveChanges();
    //     
    //     return user;
    // }
    //
    // //TODO:     VALIDATION VERIFICATION
    // public bool IsValidPassword(string password)
    // {
    //     if (string.IsNullOrWhiteSpace(password))// Spaces
    //         return false;
    //     if (password.Length < 8)// < 8
    //         return false;
    //     if (!password.Any(char.IsDigit))// 0 - 9
    //         return false;
    //     if (!password.Any(char.IsUpper))// A - Z
    //         return false;
    //     return true;
    // }
}