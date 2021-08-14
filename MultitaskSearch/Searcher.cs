using System;
using System.Collections.Generic;
using System.Linq;
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
            List<char> word = new List<char>();
            for (int i = 0; i < _chunk.Content.Length; i++)
            {
                if(_chunk.Content[i] != ' ')
                {
                    word.Add(_chunk.Content[i]);
                }
                else
                {
                    if(word.Count > 0)
                    {
                        if(_searchWords.Contains(word.ToString()))
                        {
                            _intermediateQueue.Put(word.ToString(), i + _chunk.StartIndex);
                        }
                    }
                }

            }

        }
    }
}
