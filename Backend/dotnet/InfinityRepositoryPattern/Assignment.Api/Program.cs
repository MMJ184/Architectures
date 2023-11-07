using Assignment.Api.Helpers;
using Assignment.Database;
using Assignment.Service.Common;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager Configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration["DatabaseConnection:ConnectionString"]));
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddExpressionMapping();
}, AppDomain.CurrentDomain.GetAssemblies());

builder.Services.RegisterService();
var app = builder.Build();

#region /*** Db Initialize ***/
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DatabaseContext>();
        var env = services.GetRequiredService<Microsoft.AspNetCore.Hosting.IHostingEnvironment>();
        DbInitializer.Initialize(context, env);
    }

    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}
#endregion

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
