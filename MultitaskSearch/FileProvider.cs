using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MultitaskSearch
{
    public class FileProvider : IEnumerable<Chunk>
    {
        private readonly string _filePath;
        private readonly int _dataSize;
     
        public FileProvider(string filePath, int dataSize)
        {
            _filePath = filePath;
            _dataSize = dataSize;
          
        }

        public IEnumerator<Chunk> GetEnumerator()
        {
            return GetChunks().GetEnumerator();
        }

        private IEnumerable<Chunk> GetChunks()
        {
            IEnumerable<Chunk> chunks = new List<Chunk>();
            string text = string.Empty;
            try
            {
                text = File.ReadAllText(_filePath);
            }
            catch (Exception ex)
            {
                throw new FileNotFoundException(ex + ": cannot read data from file " + _filePath);
            }

            string[] words = text.Split(new char[] { ' ', '.', ',', '!', '?', ':', ';', '[', ']', '(', ')' });
            int startIndex = 0;

            StringBuilder sb = new StringBuilder();
            foreach (var word in words)
            {
                if (sb.Length == 0 || sb.Length + word.Length < _dataSize)
                {
                    sb.Append(word);
                }
                else
                {
                    Chunk chunk = new Chunk() { Content = sb.ToString(), StartIndex = startIndex };
                    startIndex += sb.Length;
                    sb.Clear();
                    yield return chunk;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetChunks().GetEnumerator();
        }
    }
}
