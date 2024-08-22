namespace DotNet8.EmailVerification.Modules.Account.Presentation.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(
        this IServiceCollection services,
        WebApplicationBuilder builder
    )
    {
        return services
            .AddDbContextServices(builder)
            .AddDataAccessService()
            .AddHangfireService(builder)
            .AddCorsPolicyService(builder);
    }

    private static IServiceCollection AddDbContextServices(
        this IServiceCollection services,
        WebApplicationBuilder builder
    )
    {
        builder.Services.AddDbContext<AccountDbContext>(
            opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            },
            ServiceLifetime.Transient,
            ServiceLifetime.Transient
        );

        return services;
    }

    private static IServiceCollection AddDataAccessService(this IServiceCollection services)
    {
        return services.AddScoped<IUserService, UserService>();
    }

    public static FluentEmailServicesBuilder AddFluentEmail(
        this IServiceCollection services,
        WebApplicationBuilder builder
    )
    {
        var fromEmail = builder.Configuration.GetSection("FluentEmail:FromEmail")?.Value;
        return services
            .AddFluentEmail(fromEmail)
            .AddSmtpSender("smtp.gmail.com", 587, fromEmail, "wqxk dptz rfgm hjjf");
    }

    private static IServiceCollection AddHangfireService(
        this IServiceCollection services,
        WebApplicationBuilder builder
    )
    {
        builder.Services.AddHangfire(opt =>
        {
            opt.UseSqlServerStorage(builder.Configuration.GetConnectionString("DbConnection"))
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings();
        });

        builder.Services.AddHangfireServer();
        return services;
    }

    private static IServiceCollection AddCorsPolicyService(
        this IServiceCollection services,
        WebApplicationBuilder builder
    )
    {
        builder.Services.AddCors();
        return services;
    }
}
