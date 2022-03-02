using ElBayt.Common.Common;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.Mapping;
using ElBayt.Common.Security;
using ElBayt.Core.IUnitOfWork;
using ElBayt.Services.Contracts;
using ElBayt.Services.Implementations;
using ElBayt.Services.IElBaytServices;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Core.Mapping;

namespace ElBayt.Services.ElBaytServices
{
    public class ElBaytServices: IElBaytServices.IElBaytServices
    {
        private readonly IELBaytUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IUserIdentity _userIdentity;
        private readonly ITypeMapper _mapper;
        private readonly IShopMapper _shopmapper;

        public ElBaytServices( ILogger logger, IUserIdentity userIdentity, IELBaytUnitOfWork unitOfWork,
             ITypeMapper mapper, IShopMapper shopmapper)
        {
            Guard.ArgumentIsNull(unitOfWork, nameof(unitOfWork));
            Guard.ArgumentIsNull(logger, nameof(logger));
            Guard.ArgumentIsNull(userIdentity, nameof(userIdentity));
            Guard.ArgumentIsNull(mapper, nameof(mapper));
            Guard.ArgumentIsNull(shopmapper, nameof(shopmapper));
            _logger = logger;
            _userIdentity = userIdentity;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _shopmapper = shopmapper;
        }

        #region properties
        private IProductService _productService;

        private IShopService _shopService;
        #endregion

        #region Getter

        public IProductService ProductService =>
            _productService ??= new ProductService(_unitOfWork, _userIdentity, _logger, _mapper);

        public IShopService ShopService =>
            _shopService ??= new ShopService(_unitOfWork, _userIdentity, _logger, _shopmapper);

        #endregion
    }
}
