using IdentityDataApi.Data;
using IdentityDataApi.Services;
using IdentityDataApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection");
builder.Services.AddControllers();
builder.Services.AddDbContext<LoginDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<LoginDbContext>()
                .AddDefaultTokenProviders();

builder.Services.AddTransient<IAccountService, AccountService>();

builder.Services.AddAuthentication(/*JwtBearerDefaults.AuthenticationScheme*/)
    //.AddJwtBearer(options =>
    //    options.TokenValidationParameters = new TokenValidationParameters
    //    {
    //        ValidateAudience = true,
    //        ValidateIssuer = true,
    //        ValidAudience = builder.Configuration["JWTConfiguration:Audience"],
    //        ValidIssuer = builder.Configuration["JWTConfiguration:Issuer"],
    //        RequireExpirationTime = true,
    //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfiguration:SigninKey"]))
    //    })
    //.AddMicrosoftIdentityWebApi(options =>
    //{
    //    builder.Configuration.Bind("AzureAdB2C", options);
    //    options.TokenValidationParameters.NameClaimType = "name";
    //},
    //options => { builder.Configuration.Bind("AzureAdB2C", options); })
    ;


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
