namespace Shop.DataLayer.Models
{
    public enum StateOrder
    {
        Registered = 1,
        AcceptedInStock,
        IssuedToCourier,
        DeliveredPostamat,
        DeliveredToRecipient,
        Canceled,
    }
}