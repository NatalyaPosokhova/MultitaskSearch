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
        private readonly string[] _searchWords;
        
        public AbstractDirector(IDataProvider provider, int blockSize, string[] searchWords)
        {
            _provider = provider;
            _blockSize = blockSize;
            _collector = new IntermediateCollection();
            _searchWords = searchWords;
        }

        public abstract void CreateSearcher(Chunk chunk, IntermediateCollection intermediateCollection);
        public abstract Task<Dictionary<string, IList<int>>> CreateCollector(IntermediateCollection intermediateCollection);
        public async Task<Dictionary<string, IList<int>>> GetWordsPositions()
        {
            IntermediateCollection intermediateCollection = new IntermediateCollection();
            while (_provider.IsDataExists())
            {
                Chunk chunk = _provider.GetData(_blockSize);
                CreateSearcher(chunk, intermediateCollection);
            }

            var collector = await CreateCollector(intermediateCollection);
            return collector;
        }
    }
}
