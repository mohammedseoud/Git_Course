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
    public class DepartmentsServices : IDepartmentsServices
    {
        private readonly IELBaytUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IUserIdentity _userIdentity;
        private readonly ITypeMapper _mapper;

        public DepartmentsServices( ILogger logger, IUserIdentity userIdentity, IELBaytUnitOfWork unitOfWork,
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
        private IClothesService _clothesService;
        #endregion

        #region Getter

        public IClothesService ClothesService =>
           _clothesService ??= new ClothesService(_unitOfWork, _userIdentity, _logger, _mapper);


        #endregion
    }
}
