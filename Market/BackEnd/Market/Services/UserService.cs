using System.Security.Cryptography;
using System.Text;
using ShopP21.Entities;
using ShopP21.Enums;
using ShopP21.Persistence;

namespace ShopP21.Services;

public class UserService(ShopDbContext dbContext)
{
    /// <summary>
    /// Отримати користувача по ідентифікатору
    /// </summary>
    /// <param name="userId">Айді користувача</param>
    /// <returns>Користувач</returns>
    /// <exception cref="Exception">Користувача не здайено</exception>
    public User GetUserById(Guid userId)
    {
        var user = dbContext.Users.Find(userId);
        
        if (user is null)
            throw new Exception("User not found");

        return user;
    }
    
    /// <summary>
    /// Створити нового користувача
    /// </summary>
    /// <param name="phone">Телефон</param>
    /// <param name="email">Пошта</param>
    /// <param name="password">Пароль (не хешований)</param>
    /// <returns>Створений користувач</returns>
    /// <exception cref="Exception">Телефон чи емейл вже зареєстровані</exception>
    public User CreateUser(string phone, string email, string password)
    {
        if (dbContext.Users.Any(u => u.Phone == phone || u.Email == email))
            throw new Exception("User with given phone or email already exists");

        var newUser = new User
        {
            Email = email,
            Phone = phone,
            PasswordHash = HashPassword(password),
            Role = UserRole.User
        };

        dbContext.Users.Add(newUser);
        dbContext.SaveChanges();

        return newUser;
    }

    /// <summary>
    /// Отримати користувача по аутентифікаційним даним
    /// </summary>
    /// <param name="phoneOrEmail">Телефон або пошта</param>
    /// <param name="password">Пароль</param>
    /// <returns>Знайдений користувач</returns>
    /// <exception cref="Exception">Користувача не здайдено</exception>
    public User GetUserByCredentials(string phoneOrEmail, string password)
    {
        var user = dbContext.Users.FirstOrDefault(u => u.Phone == phoneOrEmail || u.Email == phoneOrEmail);

        if (user is null || user.PasswordHash != HashPassword(password))
            throw new Exception("User with given phone or email not found");

        return user;
    }

    /// <summary>
    /// Захешувати пароль алгоритмом sha56
    /// </summary>
    /// <param name="password">Сирий пароль</param>
    /// <returns>Захешований пароль</returns>
    private string HashPassword(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(bytes);
        
        var builder = new StringBuilder();
        
        foreach (var b in hashBytes)
            builder.Append(b.ToString("x2")); // "x2" — формат в нижнем регистре
        
        return builder.ToString();
    }
}