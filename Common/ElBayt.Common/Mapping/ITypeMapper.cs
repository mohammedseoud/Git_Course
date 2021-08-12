namespace ElBayt.Common.Mapping
{
    public interface ITypeMapper
    {    
        TDestination Map<TSource, TDestination>(TSource source);
    }
}