using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Blog.WebApi.Models;
using Application.Business.Abstract;
using Application.Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IContentService _contentService;
        private IPhotoService _photoService;
        private ICategoryService _categoryService;

        public ValuesController(IContentService contentService, IPhotoService photoService, ICategoryService categoryService)
        {
            _contentService = contentService;
            _photoService = photoService;
            _categoryService = categoryService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<ContentModel>> Get(int? id, string q)
        {
            var modelList = new List<ContentModel>();

            List<Content> list = _contentService.GetAll();

            if (id != null)
            {
                list = list.Where(i => i.CategoryId == id).ToList();
            }

            if (!string.IsNullOrEmpty(q))
            {
                list = list.Where(i => EF.Functions.Like(i.Title, "%" + q + "%") || EF.Functions.Like(i.Description, "%" + q + "%")).ToList();
            }


            foreach (var item in list)
            {
                var model = new ContentModel();
                model.Id = item.Id;
                model.Title = item.Title;
                model.Description = item.Description;
                model.PhotoPath = item.Photos.FirstOrDefault(p => p.IsMain == true)?.Path ?? item.Photos.FirstOrDefault(p => p.IsMain == false)?.Path;
                model.CategoryId = item.CategoryId;
                model.Category = item.Category;
                modelList.Add(model);
            }

            return Ok(modelList);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Content> Get(int id)
        {
            return _contentService.GetById(id);
        }
        
        [HttpGet("category")]
        public ActionResult<List<Content>> Category(int categoryId)
        {
            return _contentService.GetByCategory(categoryId);
        }

        [HttpGet("photo")]
        public ActionResult<List<Photo>> Photos(int contentId)
        {
            return _photoService.GetByContent(contentId);
        }

        [HttpGet("categories")]
        public ActionResult<List<Category>> Categories()
        {
            return _categoryService.GetAll();
        }

    }
}
