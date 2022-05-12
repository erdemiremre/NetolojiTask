using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetolojiMvcUI.Controllers
{
    public class SuppliersController : Controller
    {
        // GET: Suppliers
        ISupplierService _supplierService;
        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        public ActionResult Index()
        {
            var result = _supplierService.GetAll();
            return View(result.Data);
        }

        public ActionResult Add()
        {
            return PartialView("PartialAddSupplier");
        }

        [HttpPost]
        public JsonResult Add(Supplier supplier)
        {
            var result = _supplierService.Add(supplier);
            return Json(result.Success);
        }
        public ActionResult GetAll()
        {
            var result = _supplierService.GetAll();
            return PartialView("PartialListSupplier", result.Data);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var find = _supplierService.Get(id);
            var result = _supplierService.Delete(find.Data);
            return Json(result.Success);
        }


        public ActionResult Update(int id)
        {
            var result = _supplierService.Get(id);
            return View(result.Data);
        }

        [HttpPost]
        public ActionResult Update(Supplier supplier)
        {
            _supplierService.Update(supplier);
            return RedirectToAction("Index", "Suppliers");
        }
    }
}