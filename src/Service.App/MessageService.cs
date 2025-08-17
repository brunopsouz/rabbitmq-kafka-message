using Models.App.Entities;
using Repositories.App.Interfaces;
using Service.App.Interfaces;

namespace Service.App
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repository;

        public MessageService(IMessageRepository repository)
        {
            _repository = repository;
        }

        public void SendMessage(MessageModel message)
        {
            _repository.SendMessage(message);
        }
    }
}
