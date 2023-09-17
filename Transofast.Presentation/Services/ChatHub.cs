using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Transofast.Presentation.Services
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", arg1: user, message);
        }

        //public async Task SendMessage(string receiverUserId, string message)
        //{
        //    await Clients.Client("ahmet").SendAsync("ReceiveMessage", receiverUserId, message);
        //}
    }

}
