using ECommerceEnterprise.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityConfiguration();
builder.Services.AddControllersWithViews();
builder.Services.RegisterServices();
builder.Services.AddMvcConfiguration(builder.Configuration);

var app = builder.Build();

app.UseMvcConfiguration(app.Environment);
app.MapControllers();

app.Run();
