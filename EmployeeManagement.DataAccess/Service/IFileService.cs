using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Service
{
    public interface IFileService
    {
        Tuple<int, string> SaveImage(IFormFile ImageFile, string folderName);

        bool DeleteImage(string fileName, string folderName);
    }
}
