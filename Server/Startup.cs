using System.Threading.Tasks;

using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using AspNetCoreRateLimit;

using helping_hand.Data;
using helping_hand.Models;
using helping_hand.Server.Services;
using helping_hand.Data.Repository;
using helping_hand.Data.IRepository;
using helping_hand.Server.Configurations;

namespace helping_hand.Server
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
            services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("sqlConnection"))
            );
            
            services.AddMemoryCache();
            services.ConfigureRateLimiting();
            services.AddHttpContextAccessor();

            services.ConfigureHttpCacheHeaders();

            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);
            
            services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddAutoMapper(typeof(MapperInitializer));
            services.AddTransient<IAuthManager, AuthManager>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<EmailSender>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Helping Hands", Version = "v1" });
            });

            services.AddControllersWithViews().AddNewtonsoftJson(o => {
                o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.ConfigureVersioning();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Helping Hands v1"));
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.ConfigureExceptionHandler();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseCors("AllowAll");

            app.UseResponseCaching();
            app.UseHttpCacheHeaders();

            app.UseIpRateLimiting();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallback("api/{**slug}", HandleApiFallback);
                endpoints.MapFallbackToFile("{**slug}", "index.html");
            });
        }

        private Task HandleApiFallback(HttpContext context)
        {
            Error error = new()
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found"
            };

            context.Response.WriteAsync(error.ToString());
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status404NotFound;

            return Task.CompletedTask;
        }
    }
}
