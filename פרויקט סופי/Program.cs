using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using שיעור_3.Services;
using שיעור_3.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using שיעור_3.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
        .AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.TokenValidationParameters = clothesTokenServices.GetTokenValidationParameters();
        });


builder.Services.AddAuthorization(cfg =>
   {
    //    cfg.AddPolicy("Admin", policy => policy.RequireClaim("type", "Admin","User"));
    //    cfg.AddPolicy("User", policy => policy.RequireClaim("type", "User"));
     cfg.AddPolicy("Admin", policy => policy.RequireClaim("type","Admin"));
       cfg.AddPolicy("User", policy => policy.RequireClaim("type", "User","Admin"));
   });
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//להוסיף
builder.Services.AddSingleton<IClothesServices, ClothesService>();
builder.Services.AddSingleton<IUserServices, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
}

 app.UseDefaultFiles();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

