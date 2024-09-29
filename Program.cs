using MealPlannerApi.Data;
using MealPlannerApi.Models;
using MealPlannerApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MealPlannerContext>(options =>
    options.UseSqlite("Data Source=mealplanner.db"));

builder.Services.AddScoped<MealService>();
builder.Services.AddScoped<SettingsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

// Endpoints to Add, Update, Delete, and Get Meals using the MealService
app.MapPost("/meals", async (Meal meal, MealService mealService) =>
{
    await mealService.AddMealAsync(meal);
    return Results.Created($"/meals/{meal.Id}", meal);
});

app.MapPut("/meals/{id}", async (int id, Meal meal, MealService mealService) =>
{
    if (id != meal.Id)
    {
        return Results.BadRequest("Id mismatch");
    }

    await mealService.UpdateMealAsync(meal);
    return Results.Ok();
});

app.MapDelete("/meals/{id}", async (int id, MealService mealService) =>
{
    var meal = await mealService.GetMealByIdAsync(id);
    if (meal == null)
    {
        return Results.NotFound();
    }

    await mealService.DeleteMealAsync(meal);
    return Results.Ok();
});

app.MapGet("/meals", async (MealService mealService) =>
{
    return Results.Ok(await mealService.GetMealsAsync());
});

app.MapGet("/meals/{id}", async (int id, MealService mealService) =>
{
    var meal = await mealService.GetMealByIdAsync(id);
    if (meal == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(meal);
});

//Get Meals for Date

app.MapGet("/meals/date/{date}", async (string date, MealService mealService) =>
{
    Console.WriteLine($"Getting meals for date: {date}");
    return Results.Ok(await mealService.GetMealsForDateAsync(date));
});

// Settings Endpoint
app.MapGet("/settings", async (SettingsService settingsService) =>
{
    return Results.Ok(await settingsService.GetSettingsAsync());
});

app.MapPut("/settings", async (SettingsModel settings, SettingsService settingsService) =>
{
    await settingsService.UpdateSettingsAsync(settings);
    return Results.Ok();
});

app.Run();