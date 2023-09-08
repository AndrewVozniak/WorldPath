using Microsoft.AspNetCore.SignalR;

namespace Places_Service.Hubs;

public sealed class PlaceHub : Hub 
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
    }
}