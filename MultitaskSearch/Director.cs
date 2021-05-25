using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultitaskSearch
{
    public class Director : AbstractDirector
    {
        public Director(IDataProvider provider, int blockSize, char[] searchWords) : base(provider,blockSize, searchWords)
        {

        }

        public override Task<Dictionary<string, IList<int>>> CreateCollector()
        {
            throw new NotImplementedException();
        }

        public override void CreateSearcher(Chunk chunk)
        {
            throw new NotImplementedException();
        }
    }
}
