using Business.Abstract;
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

        public ActionResult GetAll()
        {
            var result = _supplierService.GetAll();
            return PartialView("PartialListSupplier", result.Data);
        }

    }
}