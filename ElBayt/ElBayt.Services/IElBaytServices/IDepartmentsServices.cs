using ElBayt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.Services.IElBaytServices
{
    public interface IDepartmentsServices
    {
        IClothesService ClothesService { get; }
    }
}
