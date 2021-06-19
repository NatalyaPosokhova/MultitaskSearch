using NUnit.Framework;
using NSubstitute;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MultitaskSearch.Tests
{
    public class IntermediateQueueTests
    {
        private IntermediateQueue intermediateQueue;

        [SetUp]
        public void Setup()
        {
            intermediateQueue = new IntermediateQueue();
        }

        [Test]
        public void InsertWordAndPositionToQueueShouldBeCorrect()
        {
            //arrange            
            intermediateQueue.Put("abcd", 25);
            KeyValuePair<string, int> expected = new KeyValuePair<string, int>("abcd", 25);

            //act
            KeyValuePair<string, int> actual = intermediateQueue.Get();

            //assert
            Assert.AreEqual(expected, actual);
        }

       [Test]
       public void InsertTwoWordsAndPositionsSecondShouldBeCorrect()
        {
            //arrange    
            intermediateQueue.Put("firstValue", 46);
            intermediateQueue.Put("secondValue", 52);
            KeyValuePair<string, int> expected = new KeyValuePair<string, int>("secondValue", 52);

            //act
            intermediateQueue.Get();
            KeyValuePair<string, int> actual = intermediateQueue.Get();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InsertTwoWordsAndPositionsTheyShouldBeInQueue()
        {
            //arrange
            KeyValuePair<string, int> expected1 = new KeyValuePair<string, int>("firstValue", 46);
            KeyValuePair<string, int> expected2 = new KeyValuePair<string, int>("secondValue", 52);
            //act
            Thread th1 = new Thread(() => intermediateQueue.Put("firstValue", 46));
            Thread th2 = new Thread(() => intermediateQueue.Put("secondValue", 52));
            th1.Start();
            th2.Start();
            th1.Join();
            th2.Join();
            //assert

            List<KeyValuePair<string, int>> actual = new List<KeyValuePair<string, int>> 
            {
                intermediateQueue.Get(),
                intermediateQueue.Get()
            };

            Assert.IsTrue(actual.Contains(expected1));
            Assert.IsTrue(actual.Contains(expected2));
        }

        [Test]
        public void QueueIsEmptyResultShouldBeZero()
        {
            //arrange
            int expected = 0;

            //act
            int actual = intermediateQueue.Count();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void QueueIsNotEmptyResultShouldBeTwo()
        {
            //arrange
            int expected = 2;
            intermediateQueue.Put("firstValue", 46);
            intermediateQueue.Put("secondValue", 52);

            //act
            int actual = intermediateQueue.Count();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test] 
        public void TryGetElementsFromEmptyQueueShouldBeException()
        {
            //arrange
            //act
            //assert
            Assert.Throws<IntermediateQueueException>(() => intermediateQueue.Get());          
        }

        [Test]
        public void TaskIsDetachedShouldBeCorrectCounter()
        {
            //arrange
            int expected = 2;

            //act
            Thread th1 = new Thread(() => intermediateQueue.DetachTask());
            Thread th2 = new Thread(() => intermediateQueue.DetachTask());
            th1.Start();
            th2.Start();
            th1.Join();
            th2.Join();

            int actual = intermediateQueue.CountDetachedTasks();

            //assert
            Assert.AreEqual(expected, actual);
        }

    }
}
