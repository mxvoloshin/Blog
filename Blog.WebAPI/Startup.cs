using System;
using System.Reflection;
using Blog.Application;
using Blog.Application.Posts.Commands.CreatePostCommand;
using Blog.Infrastructure;
using Blog.WebAPI.Middleware;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Blog.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mvcBuilder = services.AddControllers();

            AddFluentValidation(mvcBuilder);
            AddMediatrPipelines(services);

            AddDbContext(services);

            services.AddMediatR(typeof(CreatePostCommandHandler).GetTypeInfo().Assembly);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<BlogDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("BlogDB"));
                options.LogTo(Console.WriteLine);
            });
        }

        private void AddMediatrPipelines(IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        }

        private static void AddFluentValidation(IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreatePostCommandValidator>());
        }
    }
}