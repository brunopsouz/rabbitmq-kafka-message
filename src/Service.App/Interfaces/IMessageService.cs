using Models.App.Entities;

namespace Service.App.Interfaces
{
    public interface IMessageService
    {
        void SendMessage(MessageModel message);
    }
}
