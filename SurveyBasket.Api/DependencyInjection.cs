namespace SurveyBasket.Api;

public static class DependencyInjection
{
    public static IServiceCollection ApplyDependencyInjection(this IServiceCollection services)
    {
        // Add services to the container.

        services.AddControllers();
        services.AddOpenApi();


        services
            .AddFluentValidationConfig()
            .AddDependencyInjectionConfig()
            .AddMapsterConfig();



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
        services.AddScoped<IPollServices, PollServices>();

        return services;
    }

}
