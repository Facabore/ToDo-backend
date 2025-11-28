namespace ToDo_backend.Infrastructure;

#region Usings
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RideHubb.Infrastructure.Clock;
using ToDo_backend.Application.Common.Abstractions.Authentication;
using ToDo_backend.Application.Common.Abstractions.Clock;
using ToDo_backend.Application.Common.Abstractions.Data;
using ToDo_backend.Domain.Abstractions;
using ToDo_backend.Domain.Users;
using ToDo_backend.Domain.Tasks;
using ToDo_backend.Infrastructure.Authentication;
using ToDo_backend.Infrastructure.Authorization;
using ToDo_backend.Infrastructure.Database;
using ToDo_backend.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
#endregion

public static class DependencyInjection
{
    #region Constants
    private const string DbConnectionString = "PostgresConnection";
    private const string MinioOptionsSectionName = "Minio";
    private const string AllowAllPolicy = "AllowAll";
    private const string AllowSpecificOriginsPolicy = "AllowSpecificOrigins";
    private const string CorsAllowedOriginsSection = "Cors:AllowedOrigins";
    private const string POSTGRES_CONNECTION_ENV = "POSTGRES_CONNECTION";
    #endregion

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        AddPersistence(services, configuration);
        //AddStorage(services, configuration);
        AddAuthentication(services, configuration);
        AddAuthorization(services);
        AddCorsPolicy(services, configuration);

        return services;
    }

    private static void AddPersistence(
        IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresConnection") ??
                               throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString, config =>
                config.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)).UseSnakeCaseNamingConvention());

        // Persistence for entities
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<ITaskTypeRepository, TaskTypeRepository>();

        services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<IDbConnectionFactory>(_ =>
            new DbConnectionFactory(connectionString));
    }

    //private static void AddStorage(IServiceCollection services, IConfiguration configuration)
    //{
    //    var minioOptions = configuration.GetSection(MinioOptionsSectionName).Get<MinioOptions>()
    //        ?? throw new ArgumentNullException(nameof(configuration));

    //    services.AddMinio(configureClient => configureClient
    //        .WithEndpoint(minioOptions.Host)
    //        .WithCredentials(minioOptions.Username, minioOptions.Password)
    //        .WithSSL(minioOptions.IsSecureSSL)
    //        .Build());

    //    services.AddTransient<IStorageService, MinioStorageService>();
    //}

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection("Jwt").Get<JwtOptions>() ?? throw new ArgumentNullException("Jwt options");

        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
            };
        });

        services.AddHttpContextAccessor();
        services.AddTransient<IJwtProvider, JwtProvider>();
        services.AddTransient<IHashingService, HashingService>();
        services.AddScoped<IUserContext, UserContext>();
    }

    private static void AddAuthorization(IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddTransient<IClaimsTransformation, CustomClaimsTransformation>();
    }

    private static void AddCorsPolicy(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(AllowAllPolicy, policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            // After when we have a list of allowed origins, we can use it here
            options.AddPolicy(AllowSpecificOriginsPolicy, policy =>
            {
                policy
                    .WithOrigins(configuration.GetSection(CorsAllowedOriginsSection).Get<string[]>() ?? [])
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }
}