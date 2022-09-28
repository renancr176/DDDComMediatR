using System.Reflection;
using System.Text.Json;
using ApiMvno.Domain.Core.Options;
using ApiMvno.Domain.Entities;
using ApiMvno.Infra.CrossCutting.IoC;
using ApiMvno.Infra.Data.Contexts.MvnoDb;
using ApiMvno.Services.Api.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ApiMvno.Services.Api;

public class Startup : IStartup
{
    readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; private set; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
        services.AddAutoMapper(typeof(Startup).GetTypeInfo().Assembly);
        services.AddHttpContextAccessor();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "InscricaoChllApi", Version = "v1" });

            c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                In = ParameterLocation.Header,
                Description = "Jwt authorization header using the Bearer scheme."
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "bearer"
                        }
                    },
                    new string[] {}
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var filePath = Path.Combine(System.AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(filePath);
        });

        if (Environment.IsProduction())
        {
            services.AddStackExchangeRedisCache(action => {
                action.InstanceName = Configuration.GetValue<string>("RedisOptions:InstanceName");
                action.Configuration = Configuration.GetValue<string>("RedisOptions:Connection");
            });
        }
        else
        {
            services.AddDistributedMemoryCache();
        }

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.RegisterServices(Configuration);

        #region Security

        services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        /*.WithOrigins(
                            "https://apiecommerce.telecall.br",
#if DEBUG
                            , "http://localhost:3000"
#endif
                        )*/
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        services.AddIdentity<User, IdentityRole<Guid>>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
        })
        .AddEntityFrameworkStores<MvnoDbContext>()
        .AddDefaultTokenProviders();

        var appSettingJwtTokenOptions = Configuration.GetSection(JwtTokenOptions.sectionKey).Get<JwtTokenOptions>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwtOptions =>
        {
            jwtOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = !string.IsNullOrEmpty(appSettingJwtTokenOptions.Issuer),
                ValidIssuer = appSettingJwtTokenOptions.Issuer,

                ValidateAudience = !string.IsNullOrEmpty(appSettingJwtTokenOptions.Audience),
                ValidAudience = appSettingJwtTokenOptions.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = appSettingJwtTokenOptions.IssuerSigningKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };
            jwtOptions.Audience = appSettingJwtTokenOptions.Audience;
            jwtOptions.SaveToken = true;
            jwtOptions.RequireHttpsMetadata = Environment.IsDevelopment();
            jwtOptions.IncludeErrorDetails = !Environment.IsDevelopment();
            jwtOptions.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = c =>
                {
                    var response = new BaseResponse()
                    {
                        Errors = new List<BaseResponseError>()
                            {
                                new BaseResponseError()
                                {
                                    Message = Environment.IsDevelopment() ? c.Exception.Message : "An error occured processing your authentication.",
                                    ErrorCode = "InternalServerError"
                                }
                            }
                    };
                    c.NoResult();
                    c.Response.StatusCode = 500;
                    c.Response.ContentType = "application/json";

                    c.Response.WriteAsync(JsonSerializer.Serialize(response)).GetAwaiter();
                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());
        });

        #endregion
        
        Init(services.BuildServiceProvider());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InscricaoChllApi v1"));

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors(MyAllowSpecificOrigins);

        app.MapControllers();
    }

    private void Init(IServiceProvider serviceProvider)
    {
        if (Environment?.IsProduction() ?? false)
        {
            serviceProvider.MvnoDbMigrate();
        }
    }
}