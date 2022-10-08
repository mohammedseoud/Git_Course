using ElBayt.Common.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELBayt.BackOffice.Core
{
    [EnableCors(CorsOrigin.LOCAL_ORIGIN)]
    [ApiController]
    [Route("api/v1.0/ElBayt/[controller]")]
    public class ELBaytController : Controller
    {
    }
}
