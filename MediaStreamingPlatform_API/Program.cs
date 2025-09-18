using MediaStreamingPlatform_API.Application.Service;
using MediaStreamingPlatform_API.Application.UseCases;
using MediaStreamingPlatform_API.Domain.interfaces;
using MediaStreamingPlatform_API.Domain.Specifications;
using MediaStreamingPlatform_API.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var fileUploadConfig = builder.Configuration.GetSection("FileUpload");
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = fileUploadConfig.GetValue<long>("MaxFileSizeBytes");
    options.ValueLengthLimit = fileUploadConfig.GetValue<int>("ValueLengthLimit");
    options.MultipartHeadersLengthLimit = int.MaxValue;
    options.BufferBody = fileUploadConfig.GetValue<bool>("BufferBody");
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = fileUploadConfig.GetValue<long>("MaxRequestBodySizeBytes");
    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(fileUploadConfig.GetValue<int>("RequestTimeoutMinutes"));
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(fileUploadConfig.GetValue<int>("KeepAliveTimeoutMinutes"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddScoped<SignalRService>();
builder.Services.AddScoped<IPlaylistFileService, PlaylistFileService>();
builder.Services.AddScoped<IMediaPlaylistService, MediaPlaylistService>();
builder.Services.AddScoped<IMediaPlaylistRepository, MediaPlaylistRepository>();
builder.Services.AddScoped<IMediaFileService, MediaFileService>();
builder.Services.AddScoped<IMediaFileRepository, MediaFileRepository >();
builder.Services.AddScoped<IMediaFileTypeValidator, MediaFileTypeValidator >();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MSPContext>(options => {
    options.UseNpgsql(connectionString);
});

var corsConfig = builder.Configuration.GetSection("Cors");
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins(corsConfig.GetSection("AllowedOrigins").Get<string[]>())
            .AllowAnyMethod()
            .AllowAnyHeader();

        if (corsConfig.GetValue<bool>("AllowCredentials"))
        {
            policy.AllowCredentials();
        }
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

var signalRConfig = builder.Configuration.GetSection("SignalR");
app.MapHub<MediaHub>(signalRConfig.GetValue<string>("HubPath"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
