using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetolojiMvcUI.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories

        ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            var result = _categoryService.GetAll();
            return PartialView("PartialListCategory", result.Data);
        }


        public ActionResult Add()
        {
            return PartialView("PartialAddCategory");
        }

        [HttpPost]
        public JsonResult Add(Category category)
        {
            var result = _categoryService.Add(category);
            return Json(result.Success);
        }
    }
}