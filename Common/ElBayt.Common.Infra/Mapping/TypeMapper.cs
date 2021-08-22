using AutoMapper;
using ElBayt.Common ;
using ElBayt.Common.Core.Mapping;

namespace ElBayt.Common.Infra.Mapping
{
    public class TypeMapper : ITypeMapper
    {
       public TypeMapper()
        {
        }

       
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            }).CreateMapper();
            var mapping = mapper.Map<TDestination>(source);
            return mapping;
        }

       
    }
}