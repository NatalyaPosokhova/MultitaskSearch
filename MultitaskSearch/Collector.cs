using System;
using System.Collections.Generic;
using System.Text;

namespace MultitaskSearch
{
    public class Collector
    {
        private IntermediateQueue _intermediateQueue;
        private readonly int _countChunks;
        public Collector(IntermediateQueue intermediateQueue, int countChunks)
        {
            _intermediateQueue = intermediateQueue;
            _countChunks = countChunks;
        }

        internal Dictionary<string, List<int>> GetWordsAndPositionsList()
        {
            throw new NotImplementedException();
        }
    }
}
