using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BloodBankLibrary.Core.Accomodations;

using MassTransit;
using Settings;

namespace BloodBankAPI
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
                services.AddDbContext<WSDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("BloodBankDb")));



                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GraphicalEditor", Version = "v1" });
                });

                services.AddScoped<IAccomodationRepository, AccomodationRepository>();
                services.AddScoped<IAccomodationService, AccomodationService>();
                services.AddSession();
                services.AddControllers();
                services.AddMemoryCache();
            services.AddAuthentication();
            services.AddDistributedMemoryCache();
            services.AddGrpc();
        }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                app.UseCors(builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
                }

            app.UseRouting();
                app.UseSession();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGrpcService<AccomodationService>();
                });
            }
        }
    }
