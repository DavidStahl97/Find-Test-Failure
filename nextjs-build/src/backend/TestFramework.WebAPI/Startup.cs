using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using TestFramework.Application;
using TestFramework.Application.BackgroundTasks;
using TestFramework.Application.BackgroundTasks.HealthChecks;
using TestFramework.Application.BackgroundTasks.UITests;
using TestFramework.Application.Dtos;
using TestFramework.Application.Dtos.UITests.UIEvents;
using TestFramework.Application.Dtos.UITests.UITestCases;
using TestFramework.Application.Dtos.UITests.UITestRunEvents;
using TestFramework.Application.Handler.UITests.UIElements;
using TestFramework.Application.Handler.UITests.UITestRun;
using TestFramework.Application.Handler.UserFiles;
using TestFramework.Application.Pipeline;
using TestFramework.Application.UITests;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Infrastructure;
using TestFramework.Infrastructure.BackgroundTasks;
using TestFramework.Infrastructure.HealthChecks;
using TestFramework.Infrastructure.Repository.Database;
using TestFramework.Infrastructure.UITests;
using TestFramework.WebAPI.Attributes;
using TestFramework.WebAPI.Configuration;
using TestFramework.WebAPI.Extensions;
using TestFramework.WebAPI.Serialization;

namespace TestFramework.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    var converters = options.JsonSerializerOptions.Converters;
                    converters.Add(new JsonStringEnumConverter());
                    converters.Add(new JsonTimeSpanConverter());
                    converters.Add(new TypeDiscriminatorConverter<GetUITestRunEventDto>());
                    converters.Add(new TypeDiscriminatorConverter<GetUIEventDto>());
                    converters.Add(new TypeDiscriminatorConverter<PutUIEventDto>());
                });

            services.AddRazorPages();

            services.AddSwaggerGen(options => 
            {
                options.UseInlineDefinitionsForEnums();
                options.UseOneOfForPolymorphism();
                options.SelectDiscriminatorNameUsing(_ => nameof(ITypeDiscriminator.TypeDiscriminator));

                options.MapType(typeof(TimeSpan), () => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("00:00:00"),
                    Format = "time-span"
                });
            });

            services.AddInfrastructure(Configuration);

            services.AddAutoMapper(Application.Application.GetAssembly());            

            services.AddHandlers();

            services.AddScoped<IPipelineBuilder, PipelineBuilder>();          

            services.AddMediatR(Application.Application.GetAssembly());
            services.AddTransient<IParallelPublisher, ParallelPublisher>();           

            services.AddHealthChecks();

            // To-Do
            services.AddTransient<IUITestStarter, UITestStarter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestFramework");
            });

            app.UseHealthChecks("/health");

            app.AddApiRouting();
            app.AddBlazorRouting();
        }
    }
}
