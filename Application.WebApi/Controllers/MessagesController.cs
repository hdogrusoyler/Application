using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Business.Abstract;
using Application.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        private IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public ActionResult GetMessages()
        {
            return Ok(_messageService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult GetMessagesById(int id)
        {
            return Ok(_messageService.GetById(id));
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody] Message message)
        {
            _messageService.Add(message);
            return Ok(message);
        }

        [HttpPost]
        [Route("update")]
        public ActionResult Update([FromBody] Message message)
        {
            _messageService.Update(message);
            return Ok(message);
        }

        [HttpPost]
        [Route("delete")]
        public ActionResult Delete(int id)
        {
            var message = _messageService.GetById(id);
            _messageService.Delete(message);
            return Ok(message);
        }
    }
}
