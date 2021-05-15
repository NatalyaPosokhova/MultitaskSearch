using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultitaskSearch
{
    public abstract class AbstractDirector
    {
        private IDataProvider _provider;
        
        public AbstractDirector(IDataProvider provider)
        {
            _provider = provider;
        }

        public abstract Task CreateSearcher(Chunk chunk);
        public abstract Task CreateCollector();
        public Dictionary<string, int[]> GetWordsPositions()
        {
            // new NotImplementedException();
           var x = _provider.GetData(3);
            CreateSearcher(x);
            return null;
        }
    }
}
