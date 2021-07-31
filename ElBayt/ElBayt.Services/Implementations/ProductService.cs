using ElBayt.Common.Common;
using ElBayt.Common.Logging;
using ElBayt.Common.Mapping;
using ElBayt.Common.Security;
using ElBayt.Core.Entities;
using ElBayt.Core.IUnitOfWork;
using ElBayt.DTO.ELBaytDTO_s;
using ElBayt.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElBayt.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IELBaytUnitOfWork _unitOfWork;
        private readonly IUserIdentity _userIdentity;
        private readonly ILogger _logger;
        private readonly ITypeMapper _mapper;

        public ProductService(IELBaytUnitOfWork unitOfWork, IUserIdentity userIdentity, ILogger logger,
              ITypeMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task AddNewProduct(ProductDTO product)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(correlationGuid, correlationGuid, nameof(ProductService), nameof(AddNewProduct), 1, _userIdentity.Name);

                #endregion Logging info
                var Entity = _mapper.Map<ProductEntity>(product);
                await _unitOfWork.ProductRepository.AddAsync(Entity);
              
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(correlationGuid, correlationGuid, $"{nameof(ProviderPackagesService)}_{nameof(GetAllPackages)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }
    }
}
