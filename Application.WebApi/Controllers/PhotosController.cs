using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Business.Abstract;
using Application.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.Blog.WebApi.Controllers
{
    //[Route("api/[controller]")]
    //[Produces("application/json")]
    [Authorize]
    [Route("api/contents/{contentId}/photos")]
    public class PhotosController : Controller
    {
        private IPhotoService _photoService;

        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpGet]
        public ActionResult GetPhotos()
        {
            return Ok(_photoService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult GetPhotoById(int id)
        {
            return Ok(_photoService.GetById(id));
        }

        [HttpGet("content")]
        public ActionResult GetPhotoByContent(int contentId)
        {
            return Ok(_photoService.GetByContent(contentId));
        }

        [HttpPost]
        //[Route("add")]
        public ActionResult Add(int contentId)
        {
            var files = Request.Form.Files;

            var photo = new Photo();

            photo.ContentId = contentId;

            long size = files.Sum(f => f.Length);

            //var path = Path.GetTempFileName();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var path = Path.Combine(AppContext.BaseDirectory, "Images", file.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                photo.Path = "images/" + file.FileName;

                var images = _photoService.GetByContent(contentId);

                if (!images.Any(p => p.IsMain))
                {
                    photo.IsMain = true;
                }

                _photoService.Add(photo);
            }

            return Ok(new { count = files.Count, size });

        }

        [HttpPost]
        [Route("update")]
        public ActionResult Update([FromBody] Photo photo, List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            //var path = Path.GetTempFileName();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var path = Path.Combine(AppContext.BaseDirectory, "Images", file.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                photo.Path = "~/Images/" + file.FileName;

                if (_photoService.GetByContent(photo.ContentId) == null)
                {
                    photo.IsMain = true;
                }

                _photoService.Update(photo);
            }

            return Ok(new { count = files.Count, size });
        }

        [HttpPost("delete")]
        //[Route("delete")]
        public ActionResult Delete(int id)
        {
            var photo = _photoService.GetById(id);
            _photoService.Delete(photo);
            return Ok(photo);
        }
    }
}
