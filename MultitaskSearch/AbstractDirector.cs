using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultitaskSearch
{
    public abstract class AbstractDirector
    {
        private readonly IEnumerable<Chunk> _chunks;
        protected readonly IntermediateQueue _collector;
        private readonly string[] _searchWords;
        
        public AbstractDirector(IEnumerable<Chunk> chunks, string[] searchWords)
        {
            _chunks = chunks;
            _collector = new IntermediateQueue();
            _searchWords = searchWords;
        }

        public abstract void StartSearcher(string[] searchWords, Chunk chunk, IntermediateQueue intermediateQueue);
        public abstract Task<Dictionary<string, IList<int>>> CreateCollector(IntermediateQueue intermediateQueue);
        public async Task<Dictionary<string, IList<int>>> GetWordsPositions()
        {
            IntermediateQueue intermediateQueue = new IntermediateQueue();
            foreach(var chunk in _chunks)
            {
                StartSearcher(_searchWords, chunk, intermediateQueue);
            }

            var collector = await CreateCollector(intermediateQueue);
            return collector;
        }
    }
}
