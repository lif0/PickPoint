using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Shop.DataLayer.Models;
using Shop.DataLayer.Repositories.Abstracts;

namespace Shop.DataLayer.Repositories
{
    public class PostamatListRepository : IPostamatRepository
    {
        private readonly ReadOnlyCollection<Postamat> _postamats;

        public PostamatListRepository(List<Postamat> postamats)
        {
            this._postamats = new ReadOnlyCollection<Postamat>(postamats);
        }

        public PostamatResult IsExistActive(string postamatId)
        {
            var p = _postamats.FirstOrDefault(p => p.Id == postamatId);
            return p == null ? PostamatResult.NotExists : (p.IsActive ? PostamatResult.Active : PostamatResult.NotActive);
        }
        
        public Postamat FindById(string postamatId) => _postamats.FirstOrDefault(p => p.Id == postamatId);
        
        #if DEBUG
        public Postamat[] GetAll() => _postamats.ToArray();
        #endif
    }
}