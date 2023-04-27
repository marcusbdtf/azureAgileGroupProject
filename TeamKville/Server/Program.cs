using Blazored.Toast;
using Blazored.Toast.Services;
using Microsoft.EntityFrameworkCore;
using Stripe;
using TeamKville.Server.Data;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories;
using TeamKville.Server.Data.Repositories.Interfaces;

using Event = TeamKville.Server.Data.DataModels.Event;

using TeamKville.Server;
using TeamKville.Shared;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(option =>
{
	option.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
});

StripeConfiguration.ApiKey = "sk_test_51N0iM9Fb1LbkT4wodWSbNxMQoTyQjMQEUHafqk54LM2JsE5ROTWILrBwZin4z2VnhnVQiFmJMWxqdWKOPKYZLeZI00hFoMXjZm";
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

SharedClass.connectionStringBlob = builder.Configuration.GetConnectionString("connectionStringBlob");


builder.Services.AddControllers(options =>
	options.Filters.Add<ApiKeyAttribute>());

//Kommentera ovan och kommentera ut den nedanför för att testa i swagger. 
//builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IEventRepository<Event>, EventRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IMessageInterface<Message>, MessageRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
	app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAnyOrigin");
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
