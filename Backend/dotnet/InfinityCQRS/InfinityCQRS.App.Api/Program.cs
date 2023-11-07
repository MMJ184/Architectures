using AutoMapper.Extensions.ExpressionMapping;
using Cycle.App.Api.Helpers;
using FluentValidation.AspNetCore;
using InfinityCQRS.App.Api.MiddleWares;
using InfinityCQRS.App.Api.Services;
using InfinityCQRS.App.CommandResults;
using InfinityCQRS.App.Database;
using InfinityCQRS.App.Handlers.Users;
using InfinityCQRS.App.Validators.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
const string allowOrigins = "_allowOrigins";

ConfigurationManager Configuration = builder.Configuration;

// Add services to the container.
#region /*** Validator ***/
builder.Services.AddControllers().AddFluentValidation();

builder.Services.AddMvc(option =>
{
    option.EnableEndpointRouting = false;
    option.Filters.Add(typeof(BadRequestFormatAttribute));
}).AddNewtonsoftJson(options =>
{
    var converter = new StringEnumConverter(namingStrategy: new CamelCaseNamingStrategy());
    options.SerializerSettings.Converters.Add(converter);
}).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddUserCommandValidator>());
#endregion

#region /*** Swagger ***/
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation  
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "InfinityCQRS Api",
        Version = "v1",
        License = new OpenApiLicense
        {
            Name = "InfinityCQRS License",
            Url = new Uri("https://example.com/license")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    swagger.IncludeXmlComments(xmlPath);

    swagger.MapType<TimeSpan>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "timespan(hh:mm:ss)",
        Example = new OpenApiString("00:01:00")
    });

    // To Enable authorization using Swagger (JWT)  
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.",
    });

    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            }, new string[] {}
        }
    });
});
builder.Services.AddSwaggerGenNewtonsoftSupport();
#endregion

#region /*** Database ***/

builder.Services.AddDbContext<DatabaseContext>(options
    => options.UseSqlServer(Configuration["DatabaseConnection:ConnectionString"])); 
#endregion

#region /*** Add cors by config ***/
builder.Services.AddCors(options =>
{
    var origins = Configuration["AllowedOrigins"].Split(';');

    options.AddPolicy(name: allowOrigins,
                  builder =>
                  {
                      builder.WithOrigins(origins)
                             .AllowAnyMethod()
                             .AllowAnyHeader()
                             .AllowCredentials();
                  });
});
#endregion

#region /*** Mapper ***/
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddExpressionMapping();
}, AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(cfg => cfg.AddExpressionMapping(), typeof(AddUserHandler).Assembly);
#endregion

builder.Services.AddHttpContextAccessor();
builder.Services.AddRepositoryServices();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

#region /*** Db Initialize ***/
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        var context = services.GetRequiredService<DatabaseContext>();
//        var env = services.GetRequiredService<Microsoft.AspNetCore.Hosting.IHostingEnvironment>();
//        DbInitializer.Initialize(context, env);
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "An error occurred while seeding the database.");
//    }
//}
#endregion

#region /*** Swagger ***/
// Configure the HTTP request pipeline.
// TODO: In production not show swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "InfinityCQRS.App.Integration v1");
        options.RoutePrefix = string.Empty;
    });
} 
#endregion

if (app.Environment.IsDevelopment())
{
    ResponseBaseModel.IsDebuggingMode = true;
    app.UseDeveloperExceptionPage();
}
else
{
    ResponseBaseModel.IsDebuggingMode = false;
    app.UseHsts();
}

app.UseCors(allowOrigins);
app.UseAuthorization();

app.UseHttpsRedirection();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleWare>();

app.MapControllers();

app.Run();
