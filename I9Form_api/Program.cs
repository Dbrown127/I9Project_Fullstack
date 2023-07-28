﻿using I9Form_persistence;
using I9Form_persistence.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("SqlConnection"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapControllers();

using var serviceScope = app.Services.CreateScope();
var services = serviceScope.ServiceProvider;
var jsonFile = System.IO.File.ReadAllText(@"/Users/browndia/git/CapstoneNEW/i9Class_Library/AppUser/Data/AppUserSeedData.json");
var logger = services.GetRequiredService<ILogger<Program>>();

//Try and catch block for migration
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    //Try and catch blocks for seeding 
    try
    {
        //Read and store data from json in 'file'
        //Await - process won't proceed until it's finished
        await Seed.SeedData(jsonFile, context);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "AN ERROR OCCURRED WHILE SEEDING NEW DATA");
    }
}
catch (Exception ex)
{

    logger.LogError(ex, "AN ERROR OCCURRED DURING MIGRATION");
}

app.Run();

