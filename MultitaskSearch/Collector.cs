using System;
using System.Collections.Generic;
using System.Linq;
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
            Dictionary<string, List<int>> result = new Dictionary<string, List<int>>();
            List<KeyValuePair<string, int>> keyValuePairsList = new List<KeyValuePair<string, int>>();

            do
            {
                int numSearchResults = _intermediateQueue.Count();

                if (numSearchResults == 0)
                    continue;

                KeyValuePair<string, int> keyValuePair;
                for (int i = 0; i < numSearchResults; i++)
                {
                    _intermediateQueue.Get(out keyValuePair);
                    if (keyValuePair.Key != null)
                    {
                        keyValuePairsList.Add(keyValuePair);
                    }
                }
            } while (_intermediateQueue.CountDetachedTasks() < _countChunks);

            return keyValuePairsList.GroupBy(pair => pair.Key, pair => pair.Value).ToDictionary(g => g.Key, g => g.ToList());  
        }
    }
}
