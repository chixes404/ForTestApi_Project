global using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using fotTestAPI;
using fotTestAPI.DbContexts;
using fotTestAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionstring=builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.
builder.Services.AddDbContext<CityInfoContext>(options => options.UseSqlServer(connectionstring)); 

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
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

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
