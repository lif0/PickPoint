namespace Shop.DataLayer.Models
{
    public class Order
    {
        public int Id { get; set; }
        public StateOrder State { get; set; }
        public string[] Products { get; set; }
        public decimal Cost { get; set; }
        public string PostamatId { get; set; }
        public string PhoneNumber { get; set; }
        public string RecipientFullName { get; set; }
    }
}