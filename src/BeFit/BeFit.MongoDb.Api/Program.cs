using BeFit.Core;
using BeFit.MongoDb.Api;
using BeFit.MongoDb.Api.Models;
using BeFit.MongoDb.Api.Services;
using BeFit.MongoDb.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<BeFitDatabaseSettings>(
    builder.Configuration.GetSection("BeFitDatabase"));

builder.Services.AddTransient<ICategoriesService, CategoriesService>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<ITestsService, TestsService>();
builder.Services.AddTransient<ILectorsService, LectorsService>();
builder.Services.AddTransient<IAttemptsService, AttemptsService>();
builder.Services.AddTransient<ISearchService, SearchService>();

builder.Services.AddControllers();

builder.Services.AddAuthentication(option => 
{ 
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtOptions => 
{ jwtOptions.TokenValidationParameters = new TokenValidationParameters() 
    {
        ValidateActor = true,
        ValidateAudience = true, 
        ValidateLifetime = true, 
        ValidIssuer = builder.Configuration["JWTConfiguration:Issuer"], 
        ValidAudience = builder.Configuration["JWTConfiguration:Audience"], 
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWTConfiguration:SigningKey"])
            ),
    }; 
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(BefitClaimConstants.HasEvaluationPermission, policy =>
        policy.RequireClaim(BefitClaimConstants.HasEvaluationPermission, bool.TrueString));
});
    

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "BeFit API",
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement 
    {
        {
            new OpenApiSecurityScheme 
            {
                Reference = new OpenApiReference 
                {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
