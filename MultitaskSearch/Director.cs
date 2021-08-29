using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultitaskSearch
{
    public class Director : AbstractDirector
    {
        public Director(IEnumerable<Chunk> chunks, string[] searchWords) : base(chunks, searchWords)
        {

        }

        public override Task<Dictionary<string, IList<int>>> CreateCollector(IntermediateQueue intermediateQueue)
        {
            throw new NotImplementedException();
        }

        public override void StartSearcher(string[] searchWords, Chunk chunk, IntermediateQueue intermediateQueue)
        {
            throw new NotImplementedException();
        }
    }
}
