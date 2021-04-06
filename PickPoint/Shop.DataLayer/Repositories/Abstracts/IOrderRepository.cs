using Shop.DataLayer.Models;

namespace Shop.DataLayer.Repositories.Abstracts
{
    public interface IOrderRepository
    {
        Order Create(Order order);
        
        Order Update(Order order);
        
        Order FindById(int id);
    }
}