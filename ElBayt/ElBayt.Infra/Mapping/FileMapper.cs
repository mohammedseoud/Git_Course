using ElBayt.Core.Mapping;
using ElBayt.DTO.ELBayt.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ElBayt.Infra.Mapping
{
    public class FileMapper : IFileMapper
    {
        public FileMapper() { }

        public void MoveDataBetweenTwoFile(string URL1, string URL2)
        {
            var files1 = URL1.Split("\\");
            var files2 = URL2.Split("\\");

            var URL1Directory = URL1.Remove(URL1.IndexOf(files1[^1]));
            var URL2Directory = URL2.Remove(URL2.IndexOf(files2[^1]));
      
            if (Directory.Exists(URL1Directory))
            {
                if (!Directory.Exists(URL2Directory))
                {
                    var _files = Directory.GetFiles(URL1Directory);
                    var _directories = Directory.GetDirectories(URL1Directory);
                    Directory.CreateDirectory(URL2Directory);
                    foreach (var file in _files)
                    {
                        var files = file.Split("\\");
                        File.Move(file, URL2Directory + files[^1]);
                    }

                    foreach (var directory in _directories)
                    {
                        var directories = directory.Split("\\");
                        Directory.Move(directory, URL2Directory + directories[^1]);
                    }
                    Directory.Delete(URL1Directory);
                }
            }
        }

        public void MoveDataBetweenTwoProductFile(string URL1, string URL2)
        {
            var files1 = URL1.Split("\\");
            var files2 = URL2.Split("\\");
         
            var URL1Directory = URL1.Remove(URL1.IndexOf(files1[^3]));
            var URL2Directory = URL2.Remove(URL2.IndexOf(files2[^3]));
           
            if (Directory.Exists(URL1Directory))
            {
                if (!Directory.Exists(URL2Directory))
                {

                    var _files = Directory.GetFiles(URL1Directory);
                    var _directories = Directory.GetDirectories(URL1Directory);
           
                    Directory.CreateDirectory(URL2Directory);
                    foreach (var file in _files)
                    {
                        var files = file.Split("\\");
                        File.Move(file, URL2Directory + files[^1]);
                    }

                    foreach (var directory in _directories)
                    {
                        var directories = directory.Split("\\");
                        Directory.Move(directory, URL2Directory + directories[^1]);
                    }
                    Directory.Delete(URL1Directory);
                }
            }
        }
    }
}
