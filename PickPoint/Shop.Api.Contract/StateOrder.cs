namespace Shop.Api.Contract
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