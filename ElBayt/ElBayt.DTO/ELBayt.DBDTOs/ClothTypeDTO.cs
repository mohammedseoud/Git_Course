﻿using ElBayt.Common.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DBDTOs
{
    public class ClothTypeDTO : ProductTypeDTO
    {
         public Guid ClothDepartmentId { get; set; }
    }
}