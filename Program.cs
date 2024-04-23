using apiConverterHtmlToPdf.Application.Interfaces;
using apiConverterHtmlToPdf.Application.Services;
using apiConverterHtmlToPdf.Middlewares;
using apiConverterHtmlToPdf.Swagger;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IConverterService, ConverterService>();

builder.Services.AddTransient<IApiKeyValidationService, ApiKeyValidationService>();

builder.Services.AddSwaggerExtension();

var app = builder.Build();



    app.UseSwagger();

   var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwaggerUI(o=> {


        foreach (var desc in provider.ApiVersionDescriptions)
            o.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",desc.GroupName.ToUpperInvariant());
    });

app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
