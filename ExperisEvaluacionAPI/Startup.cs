using ExperisEvaluacionAPI.DbContexts;
using ExperisEvaluacionAPI.Filters;
using ExperisEvaluacionAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace ExperisEvaluacionAPI
{
    public class Startup
    {
        public IConfiguration ConfigRoot { get; }

        public Startup(IConfiguration configuration)
        {
            ConfigRoot = configuration;
        }

        private void AddAuthentication(IServiceCollection services)
        {
            var appSettingHelper = new AppSettings(ConfigRoot);

            var jwtSettings = appSettingHelper.GetJwtAppSetting();

            if (jwtSettings != null)
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.AccessTokenSecret))
                    };
                });
            }
        }

        private void AddInjetion(IServiceCollection services)
        {
            services.AddScoped(provider => new AppSettings(ConfigRoot));
            services.AddScoped(provider => new TokenService(ConfigRoot));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasherService, BcryptPasswordHasherService>();
            services.AddControllers();
            services.AddDbContext<UsuariosContext>(options => options.UseNpgsql(ConfigRoot.GetConnectionString("DefaultConnection")));

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Administración de usuarios", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.OperationFilter<AuthOperationFilter>();
            });

            AddAuthentication(services);
            AddInjetion(services);
        }

        public void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.UseCors(builder => builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod());

            app.Run();
        }

    }
}
