using EmployeeManagement.DataAccess.IRepository;
using EmployeeManagement.DataAccess.Service;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IFileService _fileService;

        public EmployeeController(IunitOfWork unitOfWork, 
            ILogger<EmployeeController> logger, IFileService fileService) {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _fileService = fileService;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _unitOfWork.Employee.GetAll();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {

            try
            {

                //create
                if (id == null || id == 0)
                {

                    return View(new Employee());
                }

                //Update
                var employee = await _unitOfWork.Employee.GetData(c => c.Id.Equals(id));
                return View(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in Upsert function: {ex.Message}");
                return View(new Employee());
            }

        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Employee employee, IFormFile? imageFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (imageFile is not null)
                    {
                        var isImageUpload = _fileService.SaveImage(imageFile, "Employee");
                        if (isImageUpload.Item1 == 0)
                        {
                            return View(employee);
                        }
                        if (!string.IsNullOrWhiteSpace(employee.ImageUrl))
                        {
                            _fileService.DeleteImage(employee.ImageUrl, "Employee");
                        }
                        employee.ImageUrl = isImageUpload.Item2;
                    }

                    //create
                    if (employee.Id == 0)
                    {
                        var isNameExist = await _unitOfWork.Employee.IsValueExit(p => p.Name.Trim().ToLower().Equals(employee.Name.Trim().ToLower()));
                        if (isNameExist)
                        {
                            ModelState.AddModelError("Name", "The Name Already Exist");
                            TempData["error"] = "The Name Already Exist";
                       
                            return View(employee);
                        }

                        await _unitOfWork.Employee.Create(employee);
                        TempData["success"] = "The Employee created successfully!";
                    }
                    //update
                    else
                    {
                        var isNameExist = await _unitOfWork.Employee.IsValueExit(p => p.Name.Trim().ToLower().Equals(employee.Name.Trim().ToLower()) && p.Id != employee.Id);
                        if (isNameExist)
                        {
                            ModelState.AddModelError("Name", "The Name Already Exist");
                            TempData["error"] = "The Name Already Exist";
                          
                            return View(employee);
                        }
                        await _unitOfWork.Employee.Update(employee);
                        TempData["success"] = "The Employee updated successfully!";
                    }
                    return RedirectToAction("Index");
                }
                return View(employee);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in Upsert post function: {ex.Message}");
                return View(employee);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var employee = await _unitOfWork.Employee.GetData(p => p.Id.Equals(id));

                if (employee is null)
                {
                    TempData["error"] = "The id is not valid!";
                    return RedirectToAction("Index");
                }

                await _unitOfWork.Employee.Delete(employee);
                TempData["success"] = "Deleted Successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in delete function: {ex.Message}");
                return RedirectToAction("Index");
            }
        }
    }
}
