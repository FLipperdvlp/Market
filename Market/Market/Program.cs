using Market.Persistence;
using Market.Services;

var app = WebApplication.Create(args);

//TODO: 🏗️ CREATE CONTEXT MANUALLY 
var dbContext = new MarketContext();

//TODO: ✅ CREATE USER_SERVICE
var userService = new UserService(dbContext);

//TODO: 🆕 ADD USER
var user = userService.AddUser("380991234567", "test@example.com", "SuperSecret123");
Console.WriteLine($"User created: {user.Id} - {user.Email}");

//TODO: 🔐 PASSWORD VERIFICATION
bool isValid = userService.VerifyPassword(user.PasswordHash, "SuperSecret123");
Console.WriteLine(isValid ? "Password is valid ✅" : "Password is invalid ❌");

//TODO: 📋 WITHDRAWAL OF ALL USERS
foreach (var u in userService.GetAllUsers())
{
    Console.WriteLine($"{u.Id} - {u.Email} - {u.Phone}");
}
//TODO: APP RUN
app.Run();