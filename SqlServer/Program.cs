using Microsoft.EntityFrameworkCore;
using SqlServer.Bussiness;
using SqlServer.DataBase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region DbConnection
builder.Services.AddDbContext<ApplicationDbContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

#region CORS
builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowVite", x => x.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod());
});
#endregion
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<OtpBussiness>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowVite");
app.UseAuthorization();

app.MapControllers();

app.Run();

