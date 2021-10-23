using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
            string wordStr;
            for (int i = 0; i < _chunk.Content.Length; i++)
            {
                if (IsAlpha(_chunk.Content[i]))
                {
                    word.Add(_chunk.Content[i]);
                }
                else
                {
                    if (word.Count > 0)
                    {
                        wordStr = new string(word.ToArray()).ToString();
                        if (_searchWords.Contains(wordStr))
                        {
                            _intermediateQueue.Put(wordStr, i - wordStr.Length + _chunk.StartIndex);
                        }
                    }
                    word.Clear();
                }
            }

            wordStr = new string(word.ToArray());
            if (_searchWords.Contains(wordStr))
            {
                _intermediateQueue.Put(wordStr, _chunk.Content.Length - wordStr.Length + _chunk.StartIndex);
            }

            _intermediateQueue.DetachTask();
        }

        private bool IsAlpha(char symbol)
        {
            return Regex.IsMatch(symbol.ToString(), @"\w");
        }
    }
}
