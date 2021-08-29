using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("MultitaskSearch.Tests")]
namespace MultitaskSearch
{
    public class IntermediateQueue
    {
        private readonly ConcurrentQueue<KeyValuePair<string, int>> _queue = new ConcurrentQueue<KeyValuePair<string, int>>();
        private int numberDequeueTasks = 0;
        internal IntermediateQueue(ConcurrentQueue<KeyValuePair<string, int>> concurrentQueue)
        {
            _queue = concurrentQueue;
        }
        public IntermediateQueue()
        {

        }

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

        public override bool Equals(object obj)
        {
            IntermediateQueue secondQueue = (IntermediateQueue)obj;
            var l1 = _queue.ToArray();
            var l2 = secondQueue._queue.ToArray();

            if(l1.Length != l2.Length)
            {
                return false;
            }

            for (int i = 0; i <l1.Length; i++)
            {
                if(l1[i].Key != l2[i].Key || l1[i].Value != l2[i].Value)
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            _queue.ToList().ForEach(x => sb.Append($"(Key: {x.Key} Value: {x.Value}) "));
            return sb.ToString();
        }
    }
}
