using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultitaskSearch
{
    public class IntermediateQueue
    {
        private KeyValuePair<string, int> _keyValuePair;

        public void Put(string word, int position)
        {
            _keyValuePair = new KeyValuePair<string, int>(word, position);
        }

        public KeyValuePair<string, int> Get()
        {
            return _keyValuePair;
        }

        public void DetachTask()
        {
            throw new NotImplementedException();
        }

        public int GetNumberDetachTasks()
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }
    }
}
