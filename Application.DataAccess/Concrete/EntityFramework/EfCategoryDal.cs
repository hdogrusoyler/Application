using System;
using System.Collections.Generic;
using System.Text;
using Application.Core.DataAccess.EntityFramework;
using Application.DataAccess.Abstract;
using Application.Entities.Concrete;

namespace Application.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, DataContext>, ICategoryDal
    {
        public EfCategoryDal(DataContext dbContext) : base(dbContext)
        {
            
        }
    }
}
