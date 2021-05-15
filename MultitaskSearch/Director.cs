using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultitaskSearch
{
    public class Director : AbstractDirector
    {
        public Director(IDataProvider provider) : base(provider)
        {

        }

        public override Task CreateCollector()
        {
            throw new NotImplementedException();
        }

        public override Task CreateSearcher(Chunk chunk)
        {
            throw new NotImplementedException();
        }
    }
}
