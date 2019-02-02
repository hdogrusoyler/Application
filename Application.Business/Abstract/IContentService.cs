using Application.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Business.Abstract
{
    public interface IContentService
    {
        Content GetById(int contentId);
        List<Content> GetAll();
        List<Content> GetByCategory(int categoryId);
        void Add(Content content);
        void Update(Content content);
        void Delete(Content content);
    }
}
