using AutoMapper;
using ElBayt.Common ;
using ElBayt.Common.Mapping;

namespace ElBayt.Common.Infra.Mapping
{
    public class TypeMapper : ITypeMapper
    {
        private readonly IMapper _mapper;

        public TypeMapper()
        {
            _mapper = Mapper.Configuration.CreateMapper();
        }

        public TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return _mapper.Map(source, destination);
        }
    }
}