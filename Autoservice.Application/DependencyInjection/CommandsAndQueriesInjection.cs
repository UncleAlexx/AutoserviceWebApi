namespace Autoservice.Application.DependencyInjection;

public static class CommandsAndQueriesInjection
{
    public static WebApplicationBuilder AddCommandsAndQueries(this WebApplicationBuilder builder) =>
        builder.AddCommonFor<CarEntity, ProductEntity>().AddCommonFor<ClientEntity, ContragentEntity>().AddCommonFor<ProviderEntity, ContragentEntity>().
        AddCommonFor<PartEntity, ProductEntity>().AddCommonFor<EmployeeEntity, EmployeeEntity>().AddContragentFor<ClientEntity>().
        AddContragentFor<ProviderEntity>().AddProductFor<CarEntity>().AddProductFor<PartEntity>();

    private static WebApplicationBuilder AddCommonFor<TEntity, TEntityBase>(this WebApplicationBuilder builder) where TEntity : TEntityBase where TEntityBase
        : EntityBase
    {
        builder.Services.AddScoped<IRequestHandler<AddCommand<TEntity>, EntityResult<TEntity>>, AddCommandHandler<TEntity, TEntityBase>>();
        builder.Services.AddScoped<IRequestHandler<ClearCommand<TEntity>, INumberBaseResult<int>>, ClearCommandHandler<TEntity>>();
        builder.Services.AddScoped<IRequestHandler<RemoveCommand<TEntity>, EntityResult<TEntity>>, RemoveCommandHandler<TEntity>>();
        builder.Services.AddScoped<IRequestHandler<UpdateCommand<TEntity>, EntityResult<TEntity>>, UpdateCommandHandler<TEntity>>();
        builder.Services.AddScoped<IRequestHandler<GetAllByIdsQuery<TEntity>, EntityCollectionResult<TEntity>>, GetAllByIdsQueryHandler<TEntity>>();
        builder.Services.AddScoped<IRequestHandler<GetAllQuery<TEntity>, EntityCollectionResult<TEntity>>, GetAllQueryHandler<TEntity>>();
        builder.Services.AddScoped<IRequestHandler<GetByIdQuery<TEntity>, EntityResult<TEntity>>, GetByIdQueryHandler<TEntity>>();
        return builder;
    }

    private static WebApplicationBuilder AddContragentFor<TContragent>(this WebApplicationBuilder builder) where TContragent : ContragentEntity
    {
        builder.Services.AddScoped<IRequestHandler<SetEmployeeCommand<TContragent>, EntityResult<EmployeeEntity>>, SetEmployeeCommandHandler<TContragent>>();
        builder.Services.AddScoped<IRequestHandler<ContragentBase.Queries.GetEmployee.GetEmployeeQuery<TContragent>, EntityResult<EmployeeEntity>>,
            ContragentBase.Queries.GetEmployee.GetEmployeeQueryHandler<TContragent>>();
        builder.Services.AddScoped<IRequestHandler<GetCarsQuery<TContragent>, EntityCollectionResult<CarEntity>>, GetCarsQueryHandler<TContragent>>();
        builder.Services.AddScoped<IRequestHandler<GetPartsQuery<TContragent>, EntityCollectionResult<PartEntity>>, GetPartsQueryHandler<TContragent>>();
        builder.Services.AddScoped<IRequestHandler<GetRevenueQuery<TContragent>, INumberBaseResult<double>>, GetRevenueQueryHandler<TContragent>>();
        return builder;
    }

    private static WebApplicationBuilder AddProductFor<TProduct>(this WebApplicationBuilder builder) where TProduct : ProductEntity
    {
        builder.Services.AddScoped<IRequestHandler<SetClientCommand<TProduct>, EntityResult<ClientEntity>>, SetClientCommandHandler<TProduct>>();
        builder.Services.AddScoped<IRequestHandler<GetProviderQuery<TProduct>, EntityResult<ProviderEntity>>, GetProviderQueryHandler<TProduct>>();
        builder.Services.AddScoped<IRequestHandler<ProductBase.Queries.GetEmployee.GetEmployeeQuery<TProduct>, EntityResult<EmployeeEntity>>,
            ProductBase.Queries.GetEmployee.GetEmployeeQueryHandler<TProduct>>();
        builder.Services.AddScoped<IRequestHandler<GetClientQuery<TProduct>, EntityResult<ClientEntity>>, GetClientQueryHandler<TProduct>>();
        builder.Services.AddScoped<IRequestHandler<SetProviderCommand<TProduct>, EntityResult<ProviderEntity>>, SetProviderCommandHandler<TProduct>>();
        return builder;
    }
   
}