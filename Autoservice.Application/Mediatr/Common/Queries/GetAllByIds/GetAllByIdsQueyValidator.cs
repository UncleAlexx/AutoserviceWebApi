namespace Autoservice.Application.Common.Queries.GetAllByIds;

public sealed class GetAllByIdsQueryValidator<TEntity> : AbstractValidator<GetAllByIdsQuery<TEntity>> where TEntity : EntityBase
{
    public GetAllByIdsQueryValidator()
    {
        RuleFor(x => x.ids).NotNull().NotEmpty();
    }
}