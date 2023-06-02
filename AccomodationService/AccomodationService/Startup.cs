using BloodBankLibrary.Core.Accomodations;
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
