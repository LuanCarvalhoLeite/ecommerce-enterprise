using ECommerceEnterprise.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddIdentityConfiguration();
builder.Services.AddControllersWithViews();
builder.Services.RegisterServices();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
