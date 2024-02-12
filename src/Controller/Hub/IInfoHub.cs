namespace RhythmsOfGiving.Controller.UI;

public interface IInfoHub
{
    Task UpdateAuctionInfo(int idLeilao);
    Task UpdateNotificationInfo(List<int> idsLicitador);
}