using Microsoft.AspNetCore.Mvc;
using Shop.DataLayer.Repositories.Abstracts;

namespace Shop.WebApi.Controllers {
    
    [ApiController]
    [Route("[controller]")]
    public class PostamatController : ControllerBase
    {
        private readonly IPostamatRepository _postamatRepository;

        public PostamatController(IPostamatRepository postamatRepository)
        {
            _postamatRepository = postamatRepository;
        }

        [HttpGet, Route("{id}")]
        public IActionResult Get(string id)
        {
            return this.Ok(_postamatRepository.FindById(id));
        }

        #if DEBUG
        [HttpGet, Route("list_DEBUG")]
        public IActionResult GetAll()
        {
            return this.Ok(_postamatRepository.GetAll());
        }
        #endif        
    }
}