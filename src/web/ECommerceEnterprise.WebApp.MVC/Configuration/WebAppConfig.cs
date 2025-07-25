﻿using ECommerceEnterprise.WebApp.MVC.Extensions;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Text.Json;

namespace ECommerceEnterprise.WebApp.MVC.Configuration;

public static class WebAppConfig
{
    public static void AddMvcConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();

        services.Configure<AppSettings>(configuration);

    }
    public static void UseMvcConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    { 
        app.UseExceptionHandler("/erro/500");
        app.UseStatusCodePagesWithRedirects("/erro/{0}");
        app.UseHsts();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseIdentityConfiguration();

        var supportedCultures = new[] { new CultureInfo("pt-BR") };
        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("pt-BR"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });

        app.UseMiddleware<ExceptionMiddleware>();

    }
}
