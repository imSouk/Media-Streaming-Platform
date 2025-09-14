using MediaStreamingPlatform_API.Application.UseCases;
using MediaStreamingPlatform_API.Domain.interfaces;
using MediaStreamingPlatform_API.Domain.Specifications;
using MediaStreamingPlatform_API.Infrastruct.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMediaFileService, MediaFileService>();
builder.Services.AddScoped<IMediaFileRepository, MediaFileRepository >();
builder.Services.AddScoped<IMediaFileTypeValidator, MediaFileTypeValidator >();
builder.Services.AddScoped<MSPContext>();
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MSPContext>(options => {
    options.UseNpgsql(connectionString);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors("AllowAll");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
