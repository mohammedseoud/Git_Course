using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DTOs
{
    public class GetColorDTO : BasicDto<Guid>
    {
        public string Name { get; set; }

    }
}
