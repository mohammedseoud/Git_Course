﻿using ElBayt.Common.Common;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElBayt.Infra.Models
{
    [Table("ClothDepartment", Schema = "dbo")]
    public class ClothDepartmentModel : ProductDepartmentModel
    {
    }
}
