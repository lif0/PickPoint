namespace Shop.Api.Contract
{
    public class Order : OrderBase
    {
        public int Id { get; set; }
        
        public StateOrder State { get; set; }
    }
}