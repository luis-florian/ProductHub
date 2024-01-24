using ProductHub.Database.Context;
using Microsoft.EntityFrameworkCore;
using ProductHub.Database.Contract;
using ProductHub.Database.Services;
using ProductHub.Storage.Contract;
using ProductHub.Storage.Services;
using ProductHub.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBContext>(op =>
{
    op.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"));

});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IImageFileService, ImageFileService>();

builder.Services.AddHttpLogging(httpLogging =>
{
    httpLogging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseCors("AllowSwagger");
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
