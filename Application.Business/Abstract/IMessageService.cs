using Application.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Business.Abstract
{
    public interface IMessageService
    {
        List<Message> GetAll();
        void Add(Message message);
        void Update(Message message);
        void Delete(Message message);
        Message GetById(int messageId);
    }
}
