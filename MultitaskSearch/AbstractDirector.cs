using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultitaskSearch
{
    public abstract class AbstractDirector
    {
        private readonly IEnumerable<Chunk> _chunks;
        private readonly string[] _searchWords;
        
        public AbstractDirector(IEnumerable<Chunk> chunks, string[] searchWords)
        {
            _chunks = chunks;
            _searchWords = searchWords;
        }

        public abstract void StartSearcher(string[] searchWords, Chunk chunk, IntermediateQueue intermediateQueue);
        public abstract Task<Dictionary<string, List<int>>> CreateCollectorAsync(IntermediateQueue intermediateQueue, int countChunks);
        public async Task<Dictionary<string, List<int>>> GetWordsPositions()
        {
            IntermediateQueue intermediateQueue = new IntermediateQueue();
            int countChunks = _chunks.Count();
            foreach(var chunk in _chunks)
            { 
                StartSearcher(_searchWords, chunk, intermediateQueue);
            }

            var collector = await CreateCollectorAsync(intermediateQueue, countChunks);
            return collector;
        }
    }
}
