using ECommerceEnterprise.Cliente.API.Configuration;
using ECommerceEnterprise.WepAPI.Core.Identidade;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddApiConfig(builder.Configuration);
builder.Services.AddRegisterService();
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
