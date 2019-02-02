using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Blog.WebApi.Models;
using Application.Business.Abstract;
using Application.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ContentsController : Controller
    {
        private IContentService _contentService;

        public ContentsController(IContentService contentService)
        {
            _contentService = contentService;
        }

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

        [HttpGet("{id}")]
        public ActionResult GetContentsById(int id)
        {
            return Ok(_contentService.GetById(id));
        }

        [HttpGet("getall")]
        public ActionResult GetContents()
        {
            return Ok(_contentService.GetAll());
        }

        [HttpGet("category")]
        public ActionResult GetContentsByCategory(int id)
        {
            return Ok(_contentService.GetByCategory(id));
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody] Content content)
        {
            _contentService.Add(content);

            return Ok(content);
        }

        [HttpPost]
        [Route("update")]
        public ActionResult Update([FromBody] Content content)
        {
            _contentService.Update(content);

            return Ok(content);
        }

        [HttpPost]
        [Route("delete")]
        public ActionResult Delete(int id)
        {
            var content = _contentService.GetById(id);
            _contentService.Delete(content);
            return Ok(content);
        }
    }
}
