using System;
using System.Collections.Generic;
using System.Text;
using Application.Business.Abstract;
using Application.DataAccess.Abstract;
using Application.Entities.Concrete;

namespace Application.Business.Concrete
{
    public class PhotoManager : IPhotoService
    {
        private IPhotoDal _photoDal;

        public PhotoManager(IPhotoDal photoDal)
        {
            _photoDal = photoDal;
        }
        public void Add(Photo photo)
        {
            _photoDal.Add(photo);
        }

        public void Delete(Photo photo)
        {
            _photoDal.Delete(photo);
        }

        public List<Photo> GetAll()
        {
            return _photoDal.GetList();
        }

        public List<Photo> GetByContent(int contentId)
        {
            return _photoDal.GetList(p => p.ContentId == contentId);
        }

        public Photo GetById(int photoId)
        {
            return _photoDal.Get(p => p.Id == photoId);
        }

        public void Update(Photo photo)
        {
            _photoDal.Update(photo);
        }
    }
}
