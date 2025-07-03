using ECommerceEnterprise.Cliente.API.Configuration;
using MediatR;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(typeof(Program));
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
