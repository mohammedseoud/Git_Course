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

namespace ElBayt.Services.ElBaytServices
{
    public class ElBaytServices: IElBaytServices.IElBaytServices
    {
        private readonly IELBaytUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IUserIdentity _userIdentity;
        private readonly ITypeMapper _mapper;

        public ElBaytServices( ILogger logger, IUserIdentity userIdentity, IELBaytUnitOfWork unitOfWork,
             ITypeMapper mapper)
        {
            Guard.ArgumentIsNull(unitOfWork, nameof(unitOfWork));
            Guard.ArgumentIsNull(logger, nameof(logger));
            Guard.ArgumentIsNull(userIdentity, nameof(userIdentity));
            Guard.ArgumentIsNull(mapper, nameof(mapper));
            _logger = logger;
            _userIdentity = userIdentity;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region properties
        private IProductService _productService;

        #endregion

        #region Getter

        public IProductService ProductService =>
             _productService ??
            (_productService = new ProductService(_unitOfWork, _userIdentity, _logger, _mapper));

        #endregion
    }
}
