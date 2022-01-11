using ElBayt.Common.Common;
using ElBayt.Common.Core.Services;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBDTOs
{
    public class GovernorateDTO : BaseDto<Guid>, IBaseDTO
    {
        public string Name { get; set; }
        public Guid CountryId { get; set; }

        public CountryDTO Countries { get; set; }
    }
}
