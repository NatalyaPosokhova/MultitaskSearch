using System;
using System.Collections.Generic;
using System.Text;

namespace MultitaskSearch
{
    public class Searcher
    {
        private readonly string[] _searchWords;
        private readonly Chunk _chunk;
        private readonly IntermediateQueue _intermediateQueue;

        public Searcher(string[] searchWords, Chunk chunk, IntermediateQueue intermediateQueue)
        {
            _searchWords = searchWords;
            _chunk = chunk;
            _intermediateQueue = intermediateQueue;
        }

        public void FindWordsAndPositions()
        {
            throw new NotImplementedException();
        }
    }
}
