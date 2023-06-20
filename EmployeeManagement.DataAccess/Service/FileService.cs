using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Service
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public bool DeleteImage(string fileName, string folderName)
        {
            try
            {
                var wwwpath = _webHostEnvironment.WebRootPath;
                var imagePath = Path.Combine(wwwpath, folderName + "\\" + fileName);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while  delete function: {ex.Message}");
                return false;
            }
        }

        public Tuple<int, string> SaveImage(IFormFile ImageFile, string folderName)
        {
            try
            {
                if (ImageFile != null)
                {
                    var wwwPath = _webHostEnvironment.WebRootPath;
                    var imagePath = Path.Combine(wwwPath, folderName);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    //check the allowed extension
                    var extension = Path.GetExtension(ImageFile.FileName);
                    var allowedExtension = new string[] { ".jpg", ".png", ".jpeg" };

                    if (!allowedExtension.Contains(extension))
                    {
                        string msg = string.Format("These extensions are support", string.Join(" ", allowedExtension));
                        return Tuple.Create(0, msg);
                    }

                    string uniqueString = Guid.NewGuid().ToString();
                    var newFileName = uniqueString + extension;
                    var fileWithPath = Path.Combine(imagePath, newFileName);
                    var stream = new FileStream(fileWithPath, FileMode.Create);
                    ImageFile.CopyTo(stream);
                    stream.Close();
                    return Tuple.Create(1, newFileName);
                }

                return Tuple.Create(0, "ImageFile is Null!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while  SaveImage function: {ex.Message}");
                return Tuple.Create(0, "something wrong");
            }
        }
    }
}
