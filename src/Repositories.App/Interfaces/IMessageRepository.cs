using Models.App.Entities;

namespace Repositories.App.Interfaces
{
    public interface IMessageRepository
    {
        void SendMessage(MessageModel message);
    }
}
