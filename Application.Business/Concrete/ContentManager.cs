using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Application.Business.Abstract;
using Application.DataAccess.Abstract;
using Application.Entities.Concrete;

namespace Application.Business.Concrete
{
    public class ContentManager : IContentService
    {
        private IContentDal _contentDal;

        public ContentManager(IContentDal contentDal)
        {
            _contentDal = contentDal;
        }

        public Content GetById(int contentId)
        {
            return _contentDal.Get(c => c.Id == contentId);
        }

        public List<Content> GetAll()
        {
            int page = 1;
            int pageSize = 0;
            return _contentDal.GetList(null, (qry) => qry.OrderByDescending(x => x.Id).ToList(), page, pageSize, i => i.Photos);
        }
        
        public List<Content> GetByCategory(int categoryId)
        {
            int page = 1;
            int pageSize = 0;
            return _contentDal.GetList(c => c.CategoryId == categoryId || categoryId == 0, (qry) => qry.OrderByDescending(x => x.Id).ToList(), page, pageSize, i => i.Photos);
        }

        public void Add(Content content)
        {
            _contentDal.Add(content);
        }

        public void Update(Content content)
        {
            _contentDal.Update(content);
        }

        public void Delete(Content content)
        {
            _contentDal.Delete(content);
        }

    }
}
