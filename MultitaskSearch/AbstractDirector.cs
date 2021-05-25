using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultitaskSearch
{
    public abstract class AbstractDirector
    {
        private readonly IDataProvider _provider;
        private readonly int _blockSize;
        protected readonly IntermediateCollection _collector;
        private readonly char[] _searchWords;
        
        public AbstractDirector(IDataProvider provider, int blockSize, char[] searchWords)
        {
            _provider = provider;
            _blockSize = blockSize;
            _collector = new IntermediateCollection();
            _searchWords = searchWords;
        }

        public abstract void CreateSearcher(Chunk chunk);
        public abstract Task<Dictionary<string, IList<int>>> CreateCollector();
        public async Task<Dictionary<string, IList<int>>> GetWordsPositions()
        {
           var t =  CreateCollector();
            while (_provider.IsDataExists())
            {
                Chunk chunk = _provider.GetData(_blockSize);
                CreateSearcher(chunk);
            }
            await t;
            return t.Result;
        }
    }
}
