using System;
using System.Collections;
using System.Collections.Generic;
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
