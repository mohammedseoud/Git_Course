using AutoMapper;
using ElBayt.Common.Core.Services;
using ElBayt.Common.Entities;
using ElBayt.Common.Infra.Models;
using ElBayt.Core.Entities;
using ElBayt.DTO.ELBaytDTO_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.Infra.Mapping
{
    public partial class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<ProductDTO, ProductEntity>();
        }



    }
}
