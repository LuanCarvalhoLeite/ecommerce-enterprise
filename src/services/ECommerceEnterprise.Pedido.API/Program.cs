using ECommerceEnterprise.Pedido.API.Configuration;
using ECommerceEnterprise.WepAPI.Core.Identidade;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddMessageBusConfiguration(builder.Configuration);
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddRegisterService();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Total");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
