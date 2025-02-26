using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using VD.Data;

var builder = WebApplication.CreateBuilder(args);
//////
if (1 < 0) // gán cứng bằng lệnh
    builder.Services.AddDbContext<StudentContext>(options =>
                options.UseSqlServer("Server=KIEN2005\\SQLEXPRESS02; Database=CMCUni2;Integrated Security=True;TrustServerCertificate=True;"));
else // lấy từ cấu hình appsettings.json
    builder.Services.AddDbContext<StudentContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentDBConnectionString")));

////////////////////////////
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) // che đi để luôn hỗ trợ Swagger
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // nếu host domant hỗ trợ https

app.UseAuthorization();

app.MapControllers();

app.Run();
