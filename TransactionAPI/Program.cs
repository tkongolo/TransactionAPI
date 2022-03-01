using TransactionAPI.Data.IRepo;
using TransactionAPI.Data.SqlRepo;
using Microsoft.EntityFrameworkCore;
using TransactionAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TransactionContext>(opt => opt.UseMySql(builder.Configuration.GetConnectionString("TransactionConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("TransactionConnection"))));

builder.Services.AddScoped<IUserLedgerRepo, SqlUserLedgerRepo>();
builder.Services.AddScoped<IUserLoginToken, SqlUserLoginToken>();
builder.Services.AddScoped<IUserResetToken, SqlUserResetToken>();
builder.Services.AddScoped<IUsers, SqlUsers>();
builder.Services.AddControllers();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TransactionContext>();
    db.Database.Migrate();
}

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
