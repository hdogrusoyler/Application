using Application.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Business.Abstract
{
    public interface IPhotoService
    {
        List<Photo> GetAll();
        List<Photo> GetByContent(int contentId);
        void Add(Photo photo);
        void Update(Photo photo);
        void Delete(Photo photo);
        Photo GetById(int photoId);
    }
}
