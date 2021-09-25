using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

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
            var result = GetChunks();
            return result.GetEnumerator();
        }

        private IEnumerable<Chunk> GetChunks()
        {
            string text = string.Empty;
            StringBuilder sb = new StringBuilder();
            var notAlfabeticSymbol = @"\W";

            try
            {
                text = File.ReadAllText(_filePath);
            }
            catch (Exception ex)
            {
                throw new FileNotFoundException(ex + ": cannot read data from file " + _filePath);
            }

            if (text.Length == 0)
                yield break;

            for (int i = 0; i < text.Length; i++)
            {
                sb.Append(text[i]);
                if (Regex.IsMatch(text[i].ToString(), notAlfabeticSymbol) && sb.Length >= _dataSize || i == text.Length - 1)
                {
                    Chunk chunk = new Chunk { Content = sb.ToString(), StartIndex = i - sb.Length + 1 };
                    sb.Clear();
                    yield return chunk;
                }
            }            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var result = this.GetChunks();
            return result.GetEnumerator();
        }
    }
}
