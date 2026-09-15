using CSharpApp.Core.Dtos.Product;

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
        async (IProductsService productsService) =>
        {
            var products = await productsService.GetAll();
            return products;
        })
    .WithName("GetProducts")
    .HasApiVersion(1.0);

versionedEndpointRouteBuilder
    .MapGet(
        "api/v{version:apiVersion}/getproduct/{id}", 
        async (IProductsService productsService, long id) =>
        {
            var products = await productsService.GetById(id);
            return products;
        })
    .WithName("GetProduct")
    .HasApiVersion(1.0);

versionedEndpointRouteBuilder
    .MapPost(
        "api/v{version:apiVersion}/createproduct",
        async (IProductsService productsService, ProductCreation product) =>
        {
            var result = await productsService.Create(product);
            return Results.Ok(result);
        })
    .WithName("CreateProduct")
    .HasApiVersion(1.0);

app.Run();