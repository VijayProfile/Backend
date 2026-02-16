using Microsoft.EntityFrameworkCore;
using SqlServer.Bussiness;
using SqlServer.DataBase;

var builder = WebApplication.CreateBuilder(args);

#region DbConnection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", x =>
        x.AllowAnyOrigin()
         .AllowAnyHeader()
         .AllowAnyMethod());
});
#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<OtpBussiness>();

var app = builder.Build();

/// ? IMPORTANT: Swagger enable in Production (Render)
app.UseSwagger();
app.UseSwaggerUI();

/// ? IMPORTANT: Render PORT binding
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Urls.Add($"http://*:{port}");

app.UseCors("AllowAll");

/// ? Disable HTTPS redirect (Render already handles HTTPS)
/// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
