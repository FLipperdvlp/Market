using Market.Middleware;
using Market.Persistence;
using Market.Services;

// var builder = WebApplication.CreateBuilder(args);
// {
//     builder.Services.AddControllers();
//     builder.Services.AddScoped<ProductService>();
//     builder.Services.AddDbContext<MarketContext>();
//
//     builder.Services.AddSwaggerGen();
// }
//
// var app = builder.Build();
// {
//     // До початку пайплайну додаємо обробник exception-ів
//     app.UseCustomExceptionHandlingMiddleware();
//     
//     app.UseSwagger();
//     app.UseSwaggerUI();
//     
//     app.MapControllers();
//     app.Run();
// }









// TODO:       BUILDER

var builder = WebApplication.CreateBuilder(args);

//TODO:      ADD CONTROLLER
builder.Services.AddControllers();

//TODO:       ADD SCOPED  PRODUCT
builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped<UserService>();

//TODO:       ADD SCOPED MARKET
builder.Services.AddScoped<MarketContext>();

//TODO:       ADD SWAGGER
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// TODO:       APP
var app = WebApplication.Create(args);

//TODO: 🆕  SWAGGER / SWAGGER UI
app.UseSwagger();
app.UseSwaggerUI();

//TODO: 🏗️ CREATE CONTEXT MANUALLY 
var dbContext = new MarketContext();

//TODO: ✅ CREATE USER_SERVICE
var userService = new UserService(dbContext);

string password = "SuperSecret123";
string password1 = "wokdfnwoefn123";
bool IsTheSame = (password == password1);  


if (IsTheSame)
{
        //TODO: 🆕 ADD USER
        var user = userService.AddUser("380672122122567", "test5@example.com", password);
        Console.WriteLine($"User created: {user.Id} - {user.Email}\nPassword is valid ✅");
}
else
{
        Console.WriteLine("User not created due to the Market Regulations\n Password is invalid ❌");
}


//TODO: 📋 WITHDRAWAL OF ALL USERS
foreach (var u in userService.GetAllUsers())
{
    Console.WriteLine($"{u.Id} - {u.Email} - {u.Phone}");
}

//TODO: APP RUN
app.Run();