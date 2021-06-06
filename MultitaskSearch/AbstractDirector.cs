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
        protected readonly IntermediateQueue _collector;
        private readonly string[] _searchWords;
        
        public AbstractDirector(IDataProvider provider, int blockSize, string[] searchWords)
        {
            _provider = provider;
            _blockSize = blockSize;
            _collector = new IntermediateQueue();
            _searchWords = searchWords;
        }

        public abstract void CreateSearcher(Chunk chunk, IntermediateQueue intermediateQueue);
        public abstract Task<Dictionary<string, IList<int>>> CreateCollector(IntermediateQueue intermediateQueue);
        public async Task<Dictionary<string, IList<int>>> GetWordsPositions()
        {
            IntermediateQueue intermediateQueue = new IntermediateQueue();
            while (_provider.IsDataExists())
            {
                Chunk chunk = _provider.GetData(_blockSize);
                CreateSearcher(chunk, intermediateQueue);
            }

            var collector = await CreateCollector(intermediateQueue);
            return collector;
        }
    }
}
