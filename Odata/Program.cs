using BusinessObjects.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Odata.Service;
using Repositories.Class;
using Repositories.Interface;
using Services.MappingProfile;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddOData(opt =>
				opt.Select().Filter().Expand().Count().OrderBy().SetMaxTop(100)
					.AddRouteComponents("odata", GetEdmModel()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

IEdmModel GetEdmModel()
{
	var builder = new ODataConventionModelBuilder();
	builder.EntitySet<Pet>("Pets");
	return builder.GetEdmModel();
}