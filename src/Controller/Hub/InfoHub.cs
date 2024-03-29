using Microsoft.AspNetCore.SignalR;

namespace RhythmsOfGiving.Controller.UI;

public class InfoHub: Hub<IInfoHub>
{
    public async Task SendUpdateAuctionMessage(int idLeilao)
    {
        await Clients.All.UpdateAuctionInfo(idLeilao);
    }
    
    public async Task SendUpdateNotificationMessage(List<int> idsLicitador)
    {
        await Clients.All.UpdateNotificationInfo(idsLicitador);
    }
}
