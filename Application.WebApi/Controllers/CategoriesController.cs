using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Business.Abstract;
using Application.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.Blog.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult GetCategories()
        {
            return Ok(_categoryService.GetAll());
        }

        [HttpGet("id")]
        public ActionResult GetCategoryById(int id)
        {
            return Ok(_categoryService.GetById(id));
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody] Category category)
        {
            _categoryService.Add(category);
            return Ok(category);
        }

        [HttpPost]
        [Route("update")]
        public ActionResult Update([FromBody] Category category)
        {
            _categoryService.Update(category);
            return Ok(category);
        }

        [HttpPost]
        [Route("delete")]
        public ActionResult Delete(int id)
        {
            Category category = _categoryService.GetById(id);
            _categoryService.Delete(category);
            return Ok(category);
        }
    }
}
