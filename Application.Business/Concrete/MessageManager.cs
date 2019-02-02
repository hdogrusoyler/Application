using System;
using System.Collections.Generic;
using System.Text;
using Application.Business.Abstract;
using Application.DataAccess.Abstract;
using Application.Entities.Concrete;

namespace Application.Business.Concrete
{
    public class MessageManager : IMessageService
    {
        private IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public void Add(Message message)
        {
            _messageDal.Add(message);
        }

        public void Delete(Message message)
        {
            _messageDal.Delete(message);
        }

        public List<Message> GetAll()
        {
            return _messageDal.GetList();
        }

        public Message GetById(int messageId)
        {
            return _messageDal.Get(m => m.Id == messageId);
        }

        public void Update(Message message)
        {
            _messageDal.Update(message);
        }
    }
}
