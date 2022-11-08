using ETicaretAPI.Application.Services;
using ETicaretAPI.Infrastructure.NameOperations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services
{
    public class FileService : IFileService
    {
        readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile files)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 2014, useAsync: false);

                await fileStream.CopyToAsync(fileStream);
                await fileStream.FlushAsync();

                return true;
            }
            catch (Exception ex)
            {
                //todo log!
                throw ex;
            }
        }

        async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
        {
            string newFileName =  await Task.Run<string>(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string newFileName = string.Empty;
                if (!first)
                {
                    string oldName = Path.GetFileNameWithoutExtension(fileName);
                    newFileName = $"{NameOperation.CharacterRegulatory(fileName)}{extension}";
                }
                else
                {
                    newFileName = fileName;

                    int indexNo1 = newFileName.IndexOf("-");
                    if (indexNo1 == -1)
                    {
                        newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                    }
                    else
                    {
                        int indexNo2 = newFileName.IndexOf(".");
                        string fileNo = newFileName.Substring(indexNo1, indexNo2 - indexNo1-1);
                        int _fileNo = int.Parse(fileNo);
                    }
                }


                if (File.Exists($"{path}\\{newFileName}"))
                {
                    return await FileRenameAsync(path, newFileName,false);
                }
                else
                {
                    return newFileName;
                }

            });

            return newFileName;
        }

        public async Task<List<(string fileName,string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,path);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            List<(string fileName, string path)> datas = new();

            List<bool> results = new();

            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(file.FileName);

                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}",file);
                datas.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
                results.Add(result);
            }

            if (results.TrueForAll(r=>r.Equals(true)))
            {
                return datas;
            }
            //todo Eğer yukarıdaki if geçerli değilse hata fırlatan mekanizma yazılacak
            return null;

        }
    }
}
