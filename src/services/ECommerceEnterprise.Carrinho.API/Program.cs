using ECommerceEnterprise.Carrinho.API.Configuration;
using ECommerceEnterprise.WepAPI.Core.Identidade;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfig(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddRegisterService();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddMessageBusConfiguration(builder.Configuration);

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
