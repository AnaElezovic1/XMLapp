using Npgsql.EntityFrameworkCore.PostgreSQL;
using BloodBankLibrary;
using BloodBankLibrary.Core.Accomodations;
using Grpc.Core;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.OpenApi.Models;
using Settings;
using Microsoft.EntityFrameworkCore;
using BloodBankAPI;
using ReservationService.Reservation;

namespace AccommodationService
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
            services.AddControllers();
            //services.AddDbContext<WSDbContext>(options =>
            //options.UseNpgsql(Configuration.GetConnectionString("BloodBankDb")), ServiceLifetime.Singleton); 
            services.Configure<DatabaseSettings>(Configuration.GetSection("Database"));
            services.AddSingleton<IReservationRepository, ReservationRepository>();
            services.AddSingleton<IReservationService, ReservationServiceBE>();
            services.AddSingleton<GRPCReservationService>();
            services.AddGrpc();
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddMvcCore().AddApiExplorer();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader()
                   ); ;
            });
            // Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        private Server server;

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Use Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowAllOrigins");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<GRPCReservationService>();
            });

            server = new Server
            {
                Services = { BloodBankAPI.ReservationService.BindService(app.ApplicationServices.GetService<GRPCReservationService>()) },
                Ports = { new ServerPort("localhost", 4311, ServerCredentials.Insecure) }
                
            };
            foreach (ServerPort s in server.Ports)
            {
                Console.WriteLine(s.Credentials);
            }
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