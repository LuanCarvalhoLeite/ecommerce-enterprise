using ECommerceEnterprise.Catalogo.API.Configuration;
using ECommerceEnterprise.WepAPI.Core.Identidade;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddApiConfig(builder.Configuration);
builder.Services.AddRegisterService();

builder.Services.AddSwaggerConfiguration();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("Total");
app.MapControllers();

app.Run();
