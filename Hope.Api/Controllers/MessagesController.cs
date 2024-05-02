using Hope.Core.Features.MessageOperation.Commands.AddMessage;
using Hope.Core.Features.MessageOperation.Commands.DeleteMessage;
using Hope.Core.Features.MessageOperation.Commands.UpdateMessage;
using Hope.Core.Features.MessageOperation.Queries.GetChatMessages;
using Hope.Core.Features.MessageOperation.Queries.GetConnectionList;
using Hope.Core.Features.MessageOperation.Queries.GetLatiestMessages;
using MediatR;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hope.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly  IMediator mediator;

        public MessagesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage(AddMessageCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMessage(DeleteMessageCommand command)
        {
            return Ok(await mediator.Send(command)); 
        }
        [HttpPost]
        public async Task<IActionResult> UpdateMessage(UpdateMessageCommand command)
        {
            return Ok(await mediator.Send(command));
        }
        [HttpGet]
        public async Task<IActionResult> GetChatMessages(string RecipientId)
        {
            return Ok(await mediator.Send(new GetChatMessagesQuery() { RecipientId=RecipientId }));
        }
        [HttpGet]
        public async Task<IActionResult> GetConnectionList()
        {
            return Ok(await mediator.Send(new GetConnectionListQuery()));
        }
        [HttpGet]
        public async Task<IActionResult> GetLatiestMessages()
        {
            return Ok(await mediator.Send(new GetLatiestMessagesQuery()));
        }
    }
}
