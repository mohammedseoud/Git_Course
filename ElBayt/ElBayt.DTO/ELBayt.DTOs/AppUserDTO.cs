using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.DTO.ELBayt.DTOs
{
    public class AppUserDTO
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public Guid Id { get; set; }
        public string AccessFailedCount { get; set; }
    }
}
