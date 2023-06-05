using BloodBankLibrary.Core.Accomodations;
using Settings;
using YourNamespace.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.OpenApi.Models;
using Grpc.Core;
using Microsoft.Extensions.Hosting.Internal;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

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
            options.UseNpgsql(Configuration.GetConnectionString("BloodBankDb")));
            var channel = new Channel("localhost", 4111, ChannelCredentials.Insecure);
            var client = new AccomodationService.AccomodationServiceClient(channel);
            services.AddSingleton(client);



            services.AddScoped<IAccomodationRepository, AccomodationRepository>();
            services.AddScoped<IAccomodationService, AccomodationServiceBE>();
            services.AddSession();
            services.AddMemoryCache();
            services.AddAuthentication();
            services.AddDistributedMemoryCache();
            services.AddGrpc();
            services.AddMvcCore().AddApiExplorer();
            services.AddSwaggerGen(options =>
            {



                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            });
            services.AddCors();
            services.AddControllers();
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
            app.UseCors(builder =>
                builder.AllowAnyOrigin()
               .AllowAnyHeader()
           .AllowAnyMethod());
            app.UseRouting();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GRPCAccomodationService>();
            });
            server = new Server
            {
                
                Services = {AccomodationService.BindService(new GRPCAccomodationService()) },
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
