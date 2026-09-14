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
            var products = await productsService.GetProducts();
            return products;
        })
    .WithName("GetProducts")
    .HasApiVersion(1.0);

app.Run();