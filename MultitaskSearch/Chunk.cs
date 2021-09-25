using System;
using System.Collections.Generic;
using System.Text;

namespace MultitaskSearch
{
    public struct Chunk
    {
        public int StartIndex;
        public string Content;

        public override bool Equals(object obj)
        {
            Chunk chunk = (Chunk)obj;
            return chunk.Content == Content && chunk.StartIndex == StartIndex;
        }
    }
}
