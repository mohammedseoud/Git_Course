namespace ElBayt.Common.Core.Mapping
{
    public interface ITypeMapper
    {    
        TDestination Map<TSource, TDestination>(TSource source);
    }
}