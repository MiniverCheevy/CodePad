using CodePad.Server.Configuration;
using CodePad.Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var corsPolicy = "corsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy,
                          policy =>
                          {
                              policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    //.AllowCredentials()
                                    ;
                          });
});


builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
           .Where(e => e.Value.Errors.Count > 0)
           .Select(e => new KeyValuePair<string, List<string>>
           (e.Key, e.Value.Errors.Select(c => c.ErrorMessage).ToList()))
           .SelectMany(e => e.Value.Select(error => new KeyValuePair<string, string>(e.Key, error)))
           .ToList();
        var reply = new Reply { IsOk = false, Message = "Please correct validation errors." };
        reply.Details = errors;

        var json = JsonSerializer.Serialize(reply);
        Debug.WriteLine("--------Model Binding Error--------");
        Debug.WriteLine(json);

        return new BadRequestObjectResult(reply);
    };
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString().Replace("+", ""));
});

builder.Services.RegisterServices();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();
app.UseCors(corsPolicy);
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
