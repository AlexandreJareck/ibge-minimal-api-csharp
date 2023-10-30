using AutoMapper;
using FluentValidation;
using IBGE.Configuration;
using IBGE.Context;
using IBGE.DTO;
using IBGE.Entities;
using IBGE.Interfaces;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.AddDbContext<IbgeDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.ConfigureDependencyInjection(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapPost("/api/location/create", async (IValidator<LocationViewModel> validator, IMapper mapper, ILocationService _locationService, LocationViewModel model, ILogger<Program> logger) =>
{
    try
    {
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
            return Results.ValidationProblem(validationResult.ToDictionary());

        var location = mapper.Map<Location>(model);
        await _locationService.Add(location);

        return Results.Ok("Location created successfully");
    }
    catch (Exception ex)
    {
        logger.LogError($"An exception occurred while processing the locality create endpoint: {ex.Message}");
        return Results.BadRequest(ex.Message);
    }
})
.WithMetadata(new SwaggerOperationAttribute(
    "Create Locale",
    "Creates a new locality. This endpoint is used to add a new locality to the system. Provide the following properties in the request body: 'Id' (string), 'State' (string), 'City' (string)."
))
.WithTags(new[] { "IBGE" });
//.RequireAuthorization();

app.Run();