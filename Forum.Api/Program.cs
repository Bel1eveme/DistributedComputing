using FluentValidation;
using Forum.Api;
using Forum.Api.Repositories;
using Forum.Api.Services;
using Forum.Api.Validation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ForumDb"));

builder.Services.AddScoped<ICreatorRepository, CreatorRepository>();
builder.Services.AddScoped<ICreatorService, CreatorService>();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddValidatorsFromAssemblyContaining<CreatorRequestDtoValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseExceptionHandler("/error");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


//ApplyMigration();

app.Run();

/*
void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}
*/

