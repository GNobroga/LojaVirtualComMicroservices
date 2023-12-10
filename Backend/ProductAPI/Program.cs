using ProductAPI.Context;
using ProductAPI.DTOs.Mappings;
using ProductAPI.Repositories;
using ProductAPI.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Services;
using ProductAPI.Services.impl;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(opt => {
    opt.SwaggerDoc("v1",new OpenApiInfo
        {
            Title = "API Product API",
            Description = "API para manipular produtos e categorias",
            Version = "0.0.1",
        });
});

builder.Services.AddAutoMapper(options => {
    options.AddProfile<MappingProfile>();
});

builder.Services.AddCors();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlite(builder.Configuration["ConnectionStrings:DefaultConnection"]);
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(options => {

    options.Run(async context => {

        var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
        var error = exceptionHandler?.Error;
        var message = "Intern error";
        context.Response.Headers.Add("Context-Type", "application/json");
        context.Response.StatusCode = 400;

        if (error is not null) 
        {   
            message = error.Message;
        }
        await context.Response.WriteAsJsonAsync(new { Title = "Error", message });
    });

});

app.UseCors(opt => opt.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());

app.MapControllers();

app.Run();
