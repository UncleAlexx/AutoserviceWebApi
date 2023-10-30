using FluentValidation.Validators;
namespace Autoservice.Application.DependencyInjection;

public static class ValidatorsInjection
{
    public static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(SetEmployeeCommandValidator<>));
        builder.Services.AddScoped(typeof(SetClientCommandValidator<>));
        builder.Services.AddScoped(typeof(SetProviderCommandValidator<>));
        builder.Services.AddScoped(typeof(EntityExistsValidator<>));
        builder.Services.AddScoped<IEntityProviderIdUniquenessValidator<ProviderEntity>, ProviderProviderUniquenessValidator>();
        builder.Services.AddScoped(typeof(IEntityProviderIdUniquenessValidator<>), typeof(EntityProviderIdUniquenessValidator<>));
        builder.Services.AddValidatorsFromAssemblyContaining<EntityExistsValidator<ClientEntity>>(includeInternalTypes : true);
        builder.Services.AddScoped(typeof(IDoesntExistValidator<>), typeof(DoesntExistValidator<>));
        builder.Services.AddScoped(typeof(GetAllByIdsQueryValidator<>));
        builder.Services.AddScoped<IAdditionValidator<EmployeeEntity>, EmployeeAdditionValidator>();
        builder.Services.AddScoped<IAdditionValidator<ContragentEntity>, ContragentBaseAdditionValidator>();
        builder.Services.AddScoped<IAdditionValidator<ProductEntity>, ProductBaseAdditionValidator>();
        builder.Services.AddScoped<IUpdateValidator<EmployeeEntity>, EmployeeUpdateValidator>();
        builder.Services.AddScoped<IPropertiesValidator<ClientEntity>, ClientPropertiesValidator>();
        builder.Services.AddScoped<IPropertiesValidator<EmployeeEntity>, EmployeePropertiesValidator>();
        builder.Services.AddScoped<IPropertiesValidator<PartEntity>, PartPropertiesValidator>();
        builder.Services.AddScoped<IPropertiesValidator<ProviderEntity>, ProviderPropertiesValidator>();
        builder.Services.AddScoped<IPropertiesValidator<CarEntity>, CarPropertiesValidator>();
        builder.Services.AddScoped<IRemoveValidator<EmployeeEntity>, EmployeeRemoveValidator>();
        builder.Services.AddScoped<IClearValidator<EmployeeEntity>, EmployeeClearValidator>();
        return builder.InjectFor<CarEntity, ProviderEntity>().InjectFor<PartEntity,ClientEntity>();
    }

    private static WebApplicationBuilder InjectFor<TProduct, TContragent>(this WebApplicationBuilder builder) where TProduct : ProductEntity 
        where TContragent : ContragentEntity
    {
        builder.Services.AddScoped<IClearValidator<TProduct>, ProductBaseClearValidator<TProduct>>();
        builder.Services.AddScoped<IClearValidator<TContragent>, ContragentBaseClearValidator<TContragent>>();
        builder.Services.AddScoped<IRemoveValidator<TProduct>, ProductBaseRemoveValidator<TProduct>>();
        builder.Services.AddScoped<IRemoveValidator<TContragent>, ContragentBaseRemoveValidator<TContragent>>();
        builder.Services.AddScoped<IUpdateValidator<TProduct>, ProductBaseUpdateValidator<TProduct>>();
        builder.Services.AddScoped<IUpdateValidator<TContragent>, ContragentBaseUpdateValidator<TContragent>>();
        return builder;
    }
}
