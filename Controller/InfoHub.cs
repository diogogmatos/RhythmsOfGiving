using Microsoft.AspNetCore.SignalR;

namespace RhythmsOfGiving.Controller;

public class InfoHub: Hub
{
    public async Task SendUpdateMessage()
    {
        await Clients.All.SendAsync("UpdateInfo");
    }
}
