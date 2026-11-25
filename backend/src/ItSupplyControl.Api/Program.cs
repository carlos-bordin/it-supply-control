using ItSupplyControl.Api.Data;
using Microsoft.EntityFrameworkCore;
using ItSupplyControl.Api.Modules.MasterData;

var builder = WebApplication.CreateBuilder(args);

// registra o AppDbContext usando sql server e a connection string "Default"
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddValidation();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapSiteEndpoints();

app.Run();
