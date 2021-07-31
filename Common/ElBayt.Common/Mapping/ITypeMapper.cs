namespace ElBayt.Common.Mapping
{
    public interface ITypeMapper
    {
        TDestination Map<TDestination>(object source);

        TDestination Map<TSource, TDestination>(TSource source);

        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}