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
        
        public bool IsExistActive(string modelPostamatId) => 
            _postamats.Any(p => p.Id == modelPostamatId && p.IsActive);
        
    }
}