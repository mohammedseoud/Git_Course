using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.DTO.ELBayt.DTOs;
using System;
using System.Collections.Generic;

namespace ElBayt.Core.Mapping
{
    public interface IFileMapper
    {
        public void MoveDataBetweenTwoFile(string URL1, string URL2);
        public void MoveDataBetweenTwoProductFile(string URL1, string URL2);
    }
}