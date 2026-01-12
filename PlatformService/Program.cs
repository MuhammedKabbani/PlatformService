using Microsoft.EntityFrameworkCore;
using PlatformService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.RegisterDbContext(builder.Environment, builder.Configuration);
builder.Services.RegisterRepositories();
builder.Services.RegisterAutoMapper();
builder.Services.RegisterHttpClients();
builder.Services.RegisterAsynClients();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.SeedData(app.Environment.IsProduction());

app.Run();

