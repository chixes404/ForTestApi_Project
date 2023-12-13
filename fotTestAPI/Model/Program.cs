global using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using fotTestAPI;
using fotTestAPI.DbContexts;
using fotTestAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.Extensions.DependencyInjection;
using fotTestAPI.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using fotTestAPI.Model.Authentication;



var builder = WebApplication.CreateBuilder(args);
var connectionstring=builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.
builder.Services.AddDbContext<CityInfoContext>(options => options.UseSqlServer(connectionstring));

//builder.Services.AddDbContext<CityInfoContext>(optionsAction: options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString(name: "DefaultConnection")));  more traditional

builder.Services.Configure<Jwt>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<CityInfoContext>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value);
    jwt.SaveToken = false;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true, // for dev -- needs to be updated when refresh token is added
        ValidateLifetime = true
    };
});


builder.Services.AddControllers(option =>
{
    option.ReturnHttpNotAcceptable=true ;    // to return xml response 
}).AddXmlDataContractSerializerFormatters();
;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<CitiesList>(); 
builder.Services.AddTransient<IMailservice,LocalMailservice>();
builder.Services.AddScoped<ICityInfoRepository,CityInfoRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
