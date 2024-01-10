using Microsoft.AspNetCore.SignalR;
using WebApi_WithBlazor_SignalR.Models;

namespace WebApi_WithBlazor_SignalR.Hubs
{
    public class SignalHub : Hub
    {
        public void BroadCastItem(Employee emp)
        {
            Clients.All.SendAsync("ReceiveEmployee", emp);
        }

        // this message will be come from client 
        public void BroadCastMessage(string message)
        {
            Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
