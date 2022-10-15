using AutoMapper;
using ElBayt.Common.Common;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.Mapping;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Common.Security;
using ElBayt.Infra.Entities;
using ElBayt.Core.IUnitOfWork;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ElBayt.Core.Models;

namespace ElBayt.Services.Implementations
{
    public class Service_Service : IService_Service
    {
        private readonly IELBaytUnitOfWork _unitOfWork;
        private readonly IUserIdentity _userIdentity;
        private readonly ILogger _logger;
        private readonly ITypeMapper _mapper;

        public Service_Service(IELBaytUnitOfWork unitOfWork, IUserIdentity userIdentity, ILogger logger,
              ITypeMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task AddNewService(ServiceDTO service)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(correlationGuid, correlationGuid, nameof(Service_Service), nameof(AddNewService), 1, _userIdentity.Name);

                #endregion Logging info
                var Entity = _mapper.Map<ServiceDTO, ServiceModel>(service);
               
                await _unitOfWork.ServiceRepository.AddAsync(Entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(correlationGuid, correlationGuid, $"{nameof(Service_Service)}_{nameof(AddNewService)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task AddNewServiceDepartment(ServiceDepartmentDTO serviceDepartment)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(serviceDepartment, correlationGuid, nameof(Service_Service), nameof(AddNewServiceDepartment), 1, _userIdentity.Name);

                #endregion Logging info
                var Entity = _mapper.Map<ServiceDepartmentDTO, ServiceDepartmentModel>(serviceDepartment);
                //Entity.Id = Guid.NewGuid();
                await _unitOfWork.ServiceDepartmentRepository.AddAsync(Entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(serviceDepartment, correlationGuid, $"{nameof(Service_Service)}_{nameof(AddNewServiceDepartment)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }
    }
}
