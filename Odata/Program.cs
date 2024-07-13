using BusinessObjects.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Odata;
using Odata.Service;
using Repositories;
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
builder.Services.AddMasterServices();
builder.Services.AddPackage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}
else
{
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Apply CORS policy
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});

app.Run();

IEdmModel GetEdmModel()
{
	var builder = new ODataConventionModelBuilder();
	builder.EntitySet<Pet>("Pets");
	builder.EntitySet<Doctor>("Doctors");
	builder.EntitySet<Schedule>("Schedules");
	return builder.GetEdmModel();
}