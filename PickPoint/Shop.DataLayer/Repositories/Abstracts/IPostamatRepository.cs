using Shop.DataLayer.Models;

namespace Shop.DataLayer.Repositories.Abstracts
{
    public interface IPostamatRepository
    {
        PostamatResult IsExistActive(string postamatId);
        
        Postamat FindById(string postamatId);
        
        #if DEBUG
        public Postamat[] GetAll();
        #endif
    }
}