namespace Autoservice.Infrastructure;

internal static class EntityTypeBuilderExtensions
{
    public static void Build<TSelfEntity, TOtherEntity>(this EntityTypeBuilder<TSelfEntity> builder, in bool many = true, in bool required = true) 
        where TSelfEntity : EntityBase where TOtherEntity : EntityBase
    {
        Trace.Assert(typeof(TSelfEntity) != typeof(TOtherEntity), $"{typeof(TSelfEntity).Name} should not be equal to {typeof(TOtherEntity).Name}");
        var CreateRequest = builder.HasOne<TOtherEntity>();
        IInfrastructure<IConventionForeignKeyBuilder>  foreignKeyLink = many ? CreateRequest.WithMany() : CreateRequest.WithOne();
        if (required)
            foreignKeyLink.Instance.IsRequired(true);
    }
}
