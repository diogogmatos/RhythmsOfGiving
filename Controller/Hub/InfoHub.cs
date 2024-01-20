using Microsoft.AspNetCore.SignalR;

namespace RhythmsOfGiving.Controller.UI;

public class InfoHub: Hub<IInfoHub>
{
    public async Task SendUpdateAuctionMessage()
    {
        await Clients.All.UpdateAuctionInfo();
    }
    
    public async Task SendUpdateNotificationMessage()
    {
        await Clients.All.UpdateNotificationInfo();
    }
}
