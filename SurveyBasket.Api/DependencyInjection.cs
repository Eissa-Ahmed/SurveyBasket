using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using SurveyBasket.Api.Middlewares;

namespace SurveyBasket.Api;

public static class DependencyInjection
{
    public static IServiceCollection ApplyDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.

        services.AddControllers();
        services.AddOpenApi();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        // Configure JwtSettings
        //services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        services.AddOptions<JwtSettings>()
            .Bind(configuration.GetSection(nameof(JwtSettings)))
            .ValidateDataAnnotations()
            .ValidateOnStart();


        services
             .AddFluentValidationConfig()
             .AddDependencyInjectionConfig()
             .AddIdentityConfig()
             .AddSqlServerConfig(configuration)
             .AddSwaggerConfig(configuration)
             .AddMapsterConfig()
             .AddCorsConfig()
             .AddAuthenticationConfig(configuration);



        return services;
    }

    private static IServiceCollection AddCorsConfig(this IServiceCollection services)
    {
        /*services
            .AddCors(options =>
            {
                options.AddPolicy("AllowAll1", builder => builder.AllowAnyHeader().WithMethods("GET", "POST", "PUT", "DELETE").AllowAnyOrigin());
                options.AddPolicy("AllowAll2", builder => builder.AllowAnyHeader().WithMethods("GET", "POST", "PUT", "DELETE").AllowAnyOrigin());

            });*/

        services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
        return services;
    }

    private static IServiceCollection AddIdentityConfig(this IServiceCollection services)
    {
        services
            .AddIdentity<ApplicationUser, IdentityRole<int>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
    private static IServiceCollection AddSqlServerConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
    private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton<IMapper>(new Mapper(mappingConfig));

        return services;
    }

    private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {
        services
           .AddFluentValidationAutoValidation()
           .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }

    private static IServiceCollection AddDependencyInjectionConfig(this IServiceCollection services)
    {
        services.AddScoped<IQuestionServices, QuestionServices>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IAuthServices, AuthServices>();
        services.AddScoped<IPollServices, PollServices>();

        return services;
    }

    private static IServiceCollection AddAuthenticationConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.SaveToken = true;
            opt.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                ValidIssuer = jwtSettings?.Issuer,
                ValidAudience = jwtSettings?.Audience
            };
        });

        return services;
    }

    private static IServiceCollection AddSwaggerConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API v1", Version = "v1.0", Description = "API v1" });
            c.EnableAnnotations();

            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer ........')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
             {
                 {
                     new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = JwtBearerDefaults.AuthenticationScheme
                         },
                         Scheme = "oauth2",
                         Name = "Bearer",
                         In = ParameterLocation.Header
                     },
                     new string[] { }
                 }
             });
        });

        return services;
    }
}
