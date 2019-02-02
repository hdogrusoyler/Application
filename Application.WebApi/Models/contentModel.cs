using Application.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Blog.WebApi.Models
{
    public class ContentModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public string PhotoPath { get; set; }
    }
}
