using CommandsService.AsyncDataServices.RabbitMQ;
using CommandsService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.WriteIndented = true;
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.RegisterDbContext(builder.Environment, builder.Configuration);
builder.Services.RegisterAutoMapper();
builder.Services.RegisterEventProccessor();
builder.Services.RegisterRepositories();
builder.Services.AddHostedService<MessageBusSubscriber>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MigrateDatabase(app.Environment.IsProduction());

app.MapControllers();

app.Run();

