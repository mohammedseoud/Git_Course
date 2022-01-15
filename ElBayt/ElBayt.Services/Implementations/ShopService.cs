using Dapper;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.Mapping;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Core.Entities;
using ElBayt.Core.IUnitOfWork;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.DTO.ELBayt.DTOs;
using ElBayt.Infra.SPs;
using ElBayt.Services.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ElBayt.Services.Implementations
{
    public class ShopService : IShopService
    {
        private readonly IELBaytUnitOfWork _unitOfWork;
        private readonly IUserIdentity _userIdentity;
        private readonly ILogger _logger;
        private readonly ITypeMapper _mapper;

        public ShopService(IELBaytUnitOfWork unitOfWork, IUserIdentity userIdentity, ILogger logger,
              ITypeMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ShopDataDTO> GetShopData(Guid DepartmentId)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(DepartmentId, correlationGuid, nameof(ShopService), nameof(GetShopData), 1, _userIdentity.Name);

                #endregion Logging info
                var ShopData = new ShopDataDTO();

                var SPParameters = new DynamicParameters();
                SPParameters.Add("@ProductDepartmentId", DepartmentId);

                var AllShopData = await _unitOfWork.SP.ListAsnyc<ProductTypesDataDTO, ShopCategoryDTO>(StoredProcedure.GETSHOPDATA, SPParameters);
                ShopData.ProductTypes = AllShopData.Item1.ToList();
                ShopData.ProductCategories = AllShopData.Item2.ToList();
                return ShopData;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(DepartmentId, correlationGuid, $"{nameof(ShopService)}_{nameof(GetShopData)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }
    }
}
