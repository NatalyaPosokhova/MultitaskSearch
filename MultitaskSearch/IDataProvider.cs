using System;
using System.Collections.Generic;
using System.Text;

namespace MultitaskSearch
{
    public interface IDataProvider
    {
        public Chunk GetData(int blockSize);
        public bool IsDataExists();
    }

    public struct Chunk
    {
        public int StartIndex;
        public string Content;
    }
}
