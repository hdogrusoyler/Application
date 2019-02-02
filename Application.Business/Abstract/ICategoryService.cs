using Application.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(int categoryId);
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}
