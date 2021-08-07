using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultitaskSearch
{
    public class IntermediateQueue
    {
        private readonly ConcurrentQueue<KeyValuePair<string, int>> _queue = new ConcurrentQueue<KeyValuePair<string, int>>();
        private int numberDequeueTasks = 0;


        public void Put(string word, int position)
        {
            KeyValuePair<string, int> item = new KeyValuePair<string, int>(word, position);
            _queue.Enqueue(item);
        }

        public bool Get(out KeyValuePair<string, int> result)
        {
            if(_queue.IsEmpty)
            {
                throw new IntermediateQueueException("Queue is empty.");
            }
           return _queue.TryDequeue(out result);            
        }

        public void DetachTask()
        {
            numberDequeueTasks++;
        }

        public int Count()
        {
            return _queue.Count;
        }

        public int CountDetachedTasks()
        {
            return numberDequeueTasks;
        }
    }
}
