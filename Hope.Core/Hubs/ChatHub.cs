using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Hope.Core.Hubs
{
    public class ChatHub:Hub
    {
        private readonly IUnitofWork work;
        private readonly UserManager<User> userManager;
        public ChatHub(IUnitofWork work, UserManager<User> userManager)
        {
            this.work = work;
            this.userManager = userManager;
        }
        public async Task SendMessage(Message message)
        {
            var userId = Context.User!.Claims.First(i => i.Type == "uid").Value;
            var user=await userManager.FindByIdAsync(userId);   
            
            user?.SentMessages.Add(message);

            var  reciver=await userManager.FindByIdAsync(message.RecipientId);

            reciver?.RecievedMessages.Add(message);
           
            var conId=await work.Repository<UserConnection>().GetItem(i=>i.UserId==message.RecipientId);

            await  Clients.Client(conId.ConnectionId).SendAsync("newMessage",message);
        }
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User!.Claims.First(i => i.Type == "uid").Value;
            await work.Repository<UserConnection>().AddAsync(new UserConnection { UserId = userId, ConnectionId = Context.ConnectionId });

            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var conId = Context.ConnectionId;

            var entity=await work.Repository<UserConnection>().GetItem(i=>i.ConnectionId== conId);

            await work.Repository<UserConnection>().Delete(entity);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
