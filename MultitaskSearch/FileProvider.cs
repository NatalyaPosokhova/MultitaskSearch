using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MultitaskSearch
{
    public class FileProvider : IEnumerable<Chunk>, IEnumerator<Chunk>
    {
        private readonly string _filePath;
        private readonly int _dataSize;

        public FileProvider(string filePath, int dataSize)
        {
            _filePath = filePath;
            _dataSize = dataSize;
        }

        private IEnumerable<Chunk> CalculateChunks()
        {
            IEnumerable<Chunk> chunks = new List<Chunk>();
            try
            {
                string text = File.ReadAllText(_filePath);
                int startIndex = 0;
                do
                {
                    Chunk chunk = new Chunk();
                    char[] content = new char[] { };
                    for (int i = startIndex; i < _dataSize || i < text.Length; i++)
                    {
                      
                    }
                } while (!text.Contains(string.Empty));

            }
            catch(Exception ex)
            {
                throw new FileNotFoundException(ex + ": cannot read data from file " + _filePath);
            }
            return chunks;
        }
        public Chunk Current => throw new NotImplementedException();

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Chunk> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
