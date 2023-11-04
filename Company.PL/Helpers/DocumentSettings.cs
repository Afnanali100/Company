using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.IO;

namespace Company.PL.Helpers
{
    public class DocumentSettings
    {
        public static string UploadImage(IFormFile file, string FolderName)
        {
            //Get Located Folder Path 
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            //Get File Name and Make It Unique
            string FileName = $"{Guid.NewGuid()}{file.FileName}";
            // Get File Path
            string FilePath=Path.Combine(FolderPath, FileName);
            //open stream
            var fs=new FileStream(FilePath,FileMode.Create);
            //send photo to stream

            file.CopyTo(fs);
            return FileName;
        }
        
        public static void DeleteFile(string FileName, string FolderName)
        {
            if (FileName is not null && FolderName is not null)
            {
                string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName, FileName);
                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                }
            }

        }

        public static string UpdateImage(string OldFileName, IFormFile NewFile,string FolderName)
        {
            DeleteFile(OldFileName, FolderName);
           return UploadImage(NewFile, FolderName);
        }



    }
}
