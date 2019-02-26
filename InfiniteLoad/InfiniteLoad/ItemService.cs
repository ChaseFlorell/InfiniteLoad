using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InfiniteLoad
{
    public class ItemService
    {
        private readonly IList<Item> _simulatedData = new List<Item>();
        public ItemService()
        {
            // simulate data in a database
            for(var i = 0; i < 200; i++)
                _simulatedData.Add(new Item{Text = $"Item {i}"});
        }
        public async Task<IEnumerable<Item>> ListItems(int skip, int take, CancellationToken token)
        {
            if(skip + take - 1 < _simulatedData.Count)
                await Task.Delay(TimeSpan.FromSeconds(2), token); // simulate latency
            
            return _simulatedData.Skip(skip).Take(take);
        }
    }
}