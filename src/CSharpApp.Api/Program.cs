using CSharpApp.Core.Dtos.Category;
using CSharpApp.Core.Dtos.Product;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging
    .ClearProviders()
    .AddSerilog(logger);

// Config
builder.Services.AddDefaultConfiguration(builder.Configuration);

// HTTP
builder.Services.AddHttpConfiguration(builder.Configuration);

// .NET
builder.Services.AddOpenApi(); 
builder.Services.AddProblemDetails();
builder.Services.AddApiVersioning();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

var versionedEndpointRouteBuilder = app.NewVersionedApi();

versionedEndpointRouteBuilder
    .MapGet(
        "api/v{version:apiVersion}/getproducts", 
        async ([FromServices] IProductsService productsService) =>
        {
            var products = await productsService.GetAll();
            return products;
        })
    .WithName("GetProducts")
    .HasApiVersion(1.0);

versionedEndpointRouteBuilder
    .MapGet(
        "api/v{version:apiVersion}/getproduct/{id}", 
        async ([FromServices] IProductsService productsService, int id) =>
        {
            var products = await productsService.GetById(id);
            return products;
        })
    .WithName("GetProduct")
    .HasApiVersion(1.0);

versionedEndpointRouteBuilder
    .MapPost(
        "api/v{version:apiVersion}/createproduct",
        async ([FromServices] IProductsService productsService, [FromBody] ProductCreation product) =>
        {
            var result = await productsService.Create(product);
            return Results.Ok(result);
        })
    .WithName("CreateProduct")
    .HasApiVersion(1.0);

versionedEndpointRouteBuilder
    .MapGet(
        "api/v{version:apiVersion}/getcategories",
        async ([FromServices] ICategoriesService categoriesService) =>
        {
            var categories = await categoriesService.GetAll();
            return categories;
        })
    .WithName("GetCategories")
    .HasApiVersion(1.0);

versionedEndpointRouteBuilder
    .MapGet(
        "api/v{version:apiVersion}/getcategory/{id}",
        async ([FromServices] ICategoriesService categoriesService, int id) =>
        {
            var category = await categoriesService.GetById(id);
            return category;
        })
    .WithName("GetCategory")
    .HasApiVersion(1.0);

versionedEndpointRouteBuilder
    .MapGet(
        "api/v{version:apiVersion}/createcategory",
        async ([FromServices] ICategoriesService categoriesService, [FromBody] CategoryCreation category) =>
        {
            var result = await categoriesService.Create(category);
            return Results.Ok(result);
        })
    .WithName("CreateCategory")
    .HasApiVersion(1.0);

app.Run();