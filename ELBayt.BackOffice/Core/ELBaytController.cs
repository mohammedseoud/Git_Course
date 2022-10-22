using ElBayt.Common.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ELBayt.BackOffice.Core
{
    [EnableCors(CorsOrigin.LOCAL_ORIGIN)]
    [ApiController]
    [Route("api/v1.0/ElBayt/[controller]")]
    public class ELBaytController : Controller
    {
        protected readonly IConfiguration _config;
        public ELBaytController( IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }
        [NonAction]
        public void SaveFile(string FilePath, int FileIndex)
        {
            var path = Path.Combine(_config["FilesInfo:Directory"], FilePath);
            var files = path.Split("\\");
            var PicDirectory = path.Remove(path.IndexOf(files[^1]));

            if (!Directory.Exists(PicDirectory))
                Directory.CreateDirectory(PicDirectory);

            using var stream = new FileStream(path, FileMode.Create);
            Request.Form.Files[FileIndex].CopyTo(stream);
        }

        [NonAction]
        public void DeleteFile(string FilePath)
        {
            var files = FilePath.Split("\\");
            var fullpath = Path.Combine(_config["FilesInfo:Directory"], FilePath.Remove(FilePath.IndexOf(files[^1])));
            if (Directory.Exists(fullpath))
                Directory.Delete(fullpath, true);
        }
        [NonAction]
        public void DeleteDirectory(string FileDirectory)
        {
            var fullpath = Path.Combine(_config["FilesInfo:Path"], FileDirectory);
            if (Directory.Exists(fullpath))
                Directory.Delete(fullpath, true);
        }
    }
}
