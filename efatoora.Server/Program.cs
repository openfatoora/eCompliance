using Agoda.IoC.NetCore;
using efatoora.service.Data;
using Microsoft.EntityFrameworkCore;
using ZatcaCore.Compliance;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<ICheckCompliances, CheckCompliances>();
builder.Services.AddDbContext<Repository>(options =>
    options.UseSqlite("Data Source=efatoora.db"));

builder.Services.AutoWireAssembly([typeof(Program).Assembly, typeof(Repository).Assembly], false);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Repository>();
    db.Database.Migrate();
    db.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
