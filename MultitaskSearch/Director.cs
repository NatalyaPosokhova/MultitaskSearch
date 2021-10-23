using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultitaskSearch
{
    public class Director : AbstractDirector
    {
        public Director(IEnumerable<Chunk> chunks, string[] searchWords) : base(chunks, searchWords)
        {

        }

        public override Task<Dictionary<string, List<int>>> CreateCollectorAsync(IntermediateQueue intermediateQueue, int countChunks)
        {
            return Task<Dictionary<string, List<int>>>.Run(() => {
                return new Collector(intermediateQueue, countChunks).GetWordsAndPositionsList();
            });
        }

        public override void StartSearcher(string[] searchWords, Chunk chunk, IntermediateQueue intermediateQueue)
        {
            Task.Run(() =>
            {
                new Searcher(searchWords, chunk, intermediateQueue).FindWordsAndPositions();
            });
        }
    }
}
