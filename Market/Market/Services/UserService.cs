using System.Security.Cryptography;
using System.Text;
using Market.Entities;
using Market.Persistence;
using Microsoft.AspNetCore.Identity;
using Konscious.Security.Cryptography;

namespace Market.Services;

public class UserService(MarketContext dbContext)
{
    private readonly PasswordHasher<User> _hasher = new();
    
    //TODO:        GET ALL USERS
    public IEnumerable<User> GetAllUsers() 
        => dbContext.Users;
    
    //TODO:        GET USER BY ID
    public User GetUserById(Guid userId)
        => dbContext.Users.Find(userId)
            ?? throw new Exception("User not found");
    
    //TODO:        GET USER BY CREDENTIALS
    public User GetUserByCredentials(string phoneOrEmail, string password)
    {
        var user = dbContext.Users.FirstOrDefault(u => u.Phone == phoneOrEmail || u.Email == phoneOrEmail);
        
        if (user is null || !VerifyArgon2(user.PasswordHash, password))
            throw new Exception("User with given phone or email not found");

        return user;
    }
    
    //TODO:       ADD USER
    public User AddUser(string phone, string email, string password)
    {
        // if (!IsValidPassword(password))
        //     throw new Exception("Password does not meet the security requirements");
        
        if (dbContext.Users.Any(u => u.Phone == phone))
            throw new Exception($"User with phone: {phone} already exists");
        
        if(dbContext.Users.Any(u => u.Email == email)) 
            throw new Exception($"User with email: {email} already exists");

        var newUser = new User
        {
            Email = email,
            Phone = phone,
            PasswordHash = "",
            Role = UserRole.User
        };
        var salt = RandomNumberGenerator.GetBytes(16);
        newUser.PasswordHash = HashWithArgon2(password, salt);
        
        dbContext.Users.Add(newUser);
        dbContext.SaveChanges();

        return newUser;
    }
    
    
    //TODO:      HASHING PASSWORD FOR BETTER SECURITY BASE DATA
    
    
    private string HashWithArgon2(string password, byte[] salt)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = 4, // Кол-во потоков (4 — хорошо для сервера)
            MemorySize = 65536,      // Память в KB (64 MB)
            Iterations = 4           // Количество итераций
        };

        var hashBytes = argon2.GetBytes(32); // Получаем 256-битный хеш
        return Convert.ToBase64String(salt) + "." + Convert.ToBase64String(hashBytes);
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
    
    public bool VerifyArgon2(string storedHash, string password)
    {
        var parts = storedHash.Split('.');
        if (parts.Length != 2) return false;

        var salt = Convert.FromBase64String(parts[0]);
        var hash = Convert.FromBase64String(parts[1]);

        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = 4,
            MemorySize = 65536,
            Iterations = 4
        };

        var attemptedHash = argon2.GetBytes(32);

        return hash.SequenceEqual(attemptedHash);
    }

    
}