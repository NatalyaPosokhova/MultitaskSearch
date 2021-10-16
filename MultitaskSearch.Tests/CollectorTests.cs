using NUnit.Framework;
using System.Collections.Generic;

namespace MultitaskSearch.Tests
{
    public class CollectorTests
    {
        private IntermediateQueue intermediateQueue;
        private Dictionary<string, List<int>> expected;

        [SetUp]
        public void Setup()
        {
            intermediateQueue = new IntermediateQueue();
            expected = new Dictionary<string, List<int>>();
        }

        [Test]
        public void CollectDataFromTwoQueuesShouldBeSuccess()
        {
            //arrange
            string word1 = "test";
            int position1 = 25;
            string word2 = "test";
            int position2 = 56;
            string word3 = "word";
            int position3 = 45;

            expected.Add(word1, new List<int> { 25, 56 });
            expected.Add(word3, new List<int> { 45 });

            intermediateQueue.Put(word1, position1);
            intermediateQueue.Put(word2, position2);
            intermediateQueue.Put(word3, position3);

            int countChunks = 2;
            var collector = new Collector(intermediateQueue, countChunks);
            intermediateQueue.DetachTask();
            intermediateQueue.DetachTask();

            //act
            Dictionary<string, List<int>> actual = collector.GetWordsAndPositionsList();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void QueueIsEmptyShouldReturnEmptyDictionary()
        {
            //arrange
            int countChunks = 2;
            var collector = new Collector(intermediateQueue, countChunks);
            intermediateQueue.DetachTask();
            intermediateQueue.DetachTask();

            //act
            Dictionary<string, List<int>> actual = collector.GetWordsAndPositionsList();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void QueueContainsOneWordAndPositionShouldBeSuccess()
        {
            //arrange
            string word1 = "test";
            int position1 = 0;

            expected.Add(word1, new List<int> { position1 });
            intermediateQueue.Put(word1, position1);

            int countChunks = 1;
            var collector = new Collector(intermediateQueue, countChunks);
            intermediateQueue.DetachTask();

            //act
            Dictionary<string, List<int>> actual = collector.GetWordsAndPositionsList();

            //assert
            Assert.AreEqual(expected, actual);
        }        

        [Test]
        public void NoDetachedTasksShouldBeException()
        {
            //arrange
            int countChunks = 0;
            var collector = new Collector(intermediateQueue, countChunks);
            Dictionary<string, List<int>> expected = new Dictionary<string, List<int>>();

            //act
            Dictionary<string, List<int>> actual = collector.GetWordsAndPositionsList();

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
