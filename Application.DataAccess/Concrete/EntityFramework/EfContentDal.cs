
using Application.Core.DataAccess.EntityFramework;
using Application.DataAccess.Abstract;
using Application.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Application.DataAccess.Concrete.EntityFramework
{
    public class EfContentDal : EfEntityRepositoryBase<Content, DataContext>, IContentDal
    {
        public EfContentDal(DataContext dbContext) : base(dbContext)
        {
            
        }

    }
}
