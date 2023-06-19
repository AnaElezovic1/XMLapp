using Settings;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.OpenApi.Models;
using Grpc.Core;
using Microsoft.Extensions.Hosting.Internal;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using BloodBankLibrary.Core.Accomodations;
using BloodBankLibrary.Core.Booking;
using YourNamespace.Services;

namespace BloodBankAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private Server server;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WSDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("BookingDb")));
            var channel = new Channel("localhost", 4111, ChannelCredentials.Insecure);
            var client = new BookingService.BookingServiceClient(channel);
            services.AddSingleton(client);



            services.AddSingleton<IBookingRepository, BookingRepository>();
            services.AddSingleton<IBookingService, BookingServiceBE>();
            services.AddSingleton<GRPCBookingService>();
            services.AddSession();
            services.AddMemoryCache();
            services.AddAuthentication();
            services.AddDistributedMemoryCache();
            services.AddGrpc();
            services.AddMvcCore().AddApiExplorer();
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            }); services.AddSwaggerGen(options =>
            {


                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
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

            }
            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseSession();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<GRPCBookingService>();
            });
            server = new Server
            {
                
                Services = {BookingService.BindService(app.ApplicationServices.GetService<GRPCBookingService>()) },
                Ports = { new ServerPort("localhost", 4111, ServerCredentials.Insecure) }
            };
            server.Start();

            applicationLifetime.ApplicationStopping.Register(OnShutdown);
        }

        private void OnShutdown()
        {
            if (server != null)
            {
                server.ShutdownAsync().Wait();
            }

        }
    }
}
