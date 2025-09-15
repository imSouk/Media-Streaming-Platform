using MediaStreamingPlatform_API.Application.Service;
using MediaStreamingPlatform_API.Application.UseCases;
using MediaStreamingPlatform_API.Domain.interfaces;
using MediaStreamingPlatform_API.Domain.Specifications;
using MediaStreamingPlatform_API.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 500 * 1024 * 1024; // 500MB
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
    options.BufferBody = false; // Para arquivos grandes
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 500 * 1024 * 1024; // 500MB
    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(10);
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMediaPlaylistService, MediaPlaylistService>();
builder.Services.AddScoped<IMediaPlaylistRepository, MediaPlaylistRepository>();
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
}
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
