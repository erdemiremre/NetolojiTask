using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetolojiMvcUI.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public ActionResult Index()
        {
            var result = _employeeService.GetAll();
            return View(result.Data);
        }

        public ActionResult Add()
        {
            return PartialView("PartialAddEmployee");
        }

        [HttpPost]
        public JsonResult Add(Employee employee)
        {
            var result = _employeeService.Add(employee);
            return Json(result.Success);
        }

        public ActionResult Update(int id)
        {
            var result = _employeeService.Get(id);
            return View(result.Data);
        }

        [HttpPost]
        public ActionResult Update(Employee employee)
        {
            _employeeService.Update(employee);
            return RedirectToAction("Index", "Employees");
        }

        public ActionResult GetAll()
        {
            var result = _employeeService.GetAll();
            return PartialView("PartialListEmployee", result.Data);
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            var find = _employeeService.Get(id);
            var result = _employeeService.Delete(find.Data);
            return Json(result.Success);
        }
    }
}