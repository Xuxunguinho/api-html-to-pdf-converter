using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace apiConverterHtmlToPdf.Swagger
{
    public static class Extensions
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddApiVersioningExtension();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerDefaultValues>();

                c.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "api-converter-html-to-pdf.xml"), true);


                c.AddSecurityDefinition("ApiKey Authentication", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Name = "apiKey", //header with api key
                    Type = SecuritySchemeType.ApiKey,

                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "ApiKey Authentication"
                                }
                            },
                            new string[] { }
                        }
                    });
            });


        }
        private static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;
                o.Conventions.Add(new VersionByNamespaceConvention());
            });
        }
    }
}
