using ElBayt.Common.Core.Services;

namespace ElBayt.DTO.ELBayt.DBUDTDTOs
{
    public class UTDProductSizeDTO : BaseDto<int>, IBaseDTO
    {
        public string Name { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }

    }
}
