namespace BASRON.Business
{
    using BASRON.Business.BTransaction.Manager;
    using BASRON.Business.BTransaction.Types;
    using BASRON.Business.BTransaction.Validator;
    //using BASRON.Business.Country.Manager;
    //using BASRON.Business.Country.Types;
    //using BASRON.Business.Country.Validator;
    using BASRON.Business.GraphQL;
    using global::GraphQL;
    using global::GraphQL.Types;
    //using Framework.Business;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Defines the <see cref="ClientBusinessDIRegistration" />.
    /// </summary>
    public static class ClientBusinessDIRegistration
    {
        /// <summary>
        /// The ConfigureBusinessServices.
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ConfigureGraphQLServices(this IServiceCollection services)
        {

            services.ConfigureGraphQLTypes()
                    .ConfigureGraphQLSchema()
                    .ConfigureGraphQLQuery()
                    .ConfigureGraphQLMutation()
                    .ConfigureGraphQLValidator();
                    

            services.AddGraphQL(b => b
            .AddGraphTypes(typeof(AppSchema).Assembly) // schema            
                .AddSystemTextJson()).AddGraphQLUpload();

            return services;
        }

        private static IServiceCollection ConfigureGraphQLTypes(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<BTransactionType>()
                .AddClasses(classes => classes.Where(type => type.IsClass))
                .AsSelf().WithScopedLifetime());

            return services;
        }

        private static IServiceCollection ConfigureGraphQLSchema(this IServiceCollection services)
        {
            services.AddScoped<ISchema, AppSchema>();
            services.AddScoped<AppSchema>();
            return services;
        }

        private static IServiceCollection ConfigureGraphQLQuery(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<BTransactionQuery>() // Adjust assembly if needed
                .AddClasses(classes => classes.AssignableTo<ITopLevelQuery>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }

        private static IServiceCollection ConfigureGraphQLMutation(this IServiceCollection services)
        {
            services.Scan(scan => scan
               .FromAssemblyOf<BTransactionMutation>() // Adjust assembly if needed
               .AddClasses(classes => classes.AssignableTo<ITopLevelMutation>())
               .AsImplementedInterfaces()
               .WithScopedLifetime());

            return services;
        }

        private static IServiceCollection ConfigureGraphQLValidator(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<BTransactionCreateModelValidator>()
                .AddClasses(classes => classes.Where(type => type.IsClass))
                .AsSelf().WithScopedLifetime());
            return services;
        }
    }
}
