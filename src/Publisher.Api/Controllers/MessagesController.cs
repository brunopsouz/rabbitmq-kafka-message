using Microsoft.AspNetCore.Mvc;
using Models.App.Entities;
using Service.App.Interfaces;

namespace Publisher.Api.Controllers
{
    [Route("api/message")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _service;

        public MessagesController(IMessageService service)
        {
            _service = service;
        }

        [HttpPost]
        public void AddMessage(MessageModel message)
        {
            _service.SendMessage(message);
        }
    }
}
