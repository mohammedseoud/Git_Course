namespace ElBayt.Common.Core.Services
{
    public class ProductSizeDTO : BasicDto<int>, IBaseDTO
    { 
        public string Name { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }

    }
}
