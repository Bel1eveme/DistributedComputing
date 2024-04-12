using FluentValidation;
using Forum.PostApi.Extensions;
using Forum.PostApi.Models;
using Forum.PostApi.Models.Dto;
using Forum.PostApi.Repositories;
using Forum.PostApi.Repositories.Base;
using Forum.PostApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterCassandra();

builder.Services.AddScoped<IBaseRepository<Post, long>, BaseRepository<Post, long>>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<PostRequestDto>();

var app = builder.Build();

app.UseExceptionHandler(new ExceptionHandlerOptions
{
    AllowStatusCode404Response = true,
    ExceptionHandlingPath = "/error"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();