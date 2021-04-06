using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Contract;
using Shop.DataLayer.Repositories.Abstracts;
using Shop.WebApi.Models;

namespace Shop.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ShopControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPostamatRepository _postamatRepository;

        public OrderController(IMapper mapper, IOrderRepository orderRepository, IPostamatRepository postamatRepository) : base(mapper)
        {
            _orderRepository = orderRepository;
            _postamatRepository = postamatRepository;
        }

        public OrderManager Manager => new OrderManager(Mapper, _orderRepository, _postamatRepository);

        [HttpPost]
        public IActionResult Create(OrderCreate model) =>
            OkOrErrorResult(Manager.Create(model));

        [HttpPut, Route("{id:int}")]
        public IActionResult Update(int id, OrderUpdate model) => 
            OkOrErrorResult(Manager.Update(id, model));
        
        [HttpGet, Route("{id:int}/cancel")]
        public IActionResult Cancel(int id) => 
            OkOrErrorResult(Manager.Cancel(id));
        
        [HttpGet, Route("{id:int}")]
        public IActionResult Get(int id) => 
            OkOrErrorResult(Manager.FindById(id));
        
        #if DEBUG
        [HttpGet, Route("list_DEBUG")]
        public IActionResult GetAll()
        {
            return this.Ok(_orderRepository.GetAll());
        }
        #endif 
    }
}