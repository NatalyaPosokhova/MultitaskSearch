using System;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace MultitaskSearch.Tests
{
    public class SearcherTests
    {
        private IntermediateQueue _intermediateQueue;

        [SetUp]
        public void SetUp()
        {
            _intermediateQueue = new IntermediateQueue();
        }
        IEnumerable<TestCaseData> GetSeacherData()
        {
            yield return new TestCaseData(new string[] { "abc" }, new Chunk() { StartIndex = 0, Content = " fds abc tyu" },
                new ConcurrentQueue<KeyValuePair<string, int>>(new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("abc", 6) }));

            yield return new TestCaseData(new string[] { "abc, def" }, new Chunk() { StartIndex = 0, Content = "abc tyu" },
                new ConcurrentQueue<KeyValuePair<string, int>>(new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("abc", 1) }));

            yield return new TestCaseData(new string[] { "abc, def" }, new Chunk() { StartIndex = 0, Content = " fds def tyu abc" },
                new ConcurrentQueue<KeyValuePair<string, int>>(new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("abc", 14), new KeyValuePair<string, int>("def", 6) }));

            yield return new TestCaseData(new string[] { "abc, def" }, new Chunk() { StartIndex = 0, Content = " fds deft tyuabc" },
                new ConcurrentQueue<KeyValuePair<string, int>>());

            yield return new TestCaseData(new string[] { "abc, def" }, new Chunk() { StartIndex = 0, Content = " fds deft tyuabc" },
                new ConcurrentQueue<KeyValuePair<string, int>>());
        }
        [Test]
        [TestCaseSource("GetSeacherData")]
        public void FindWordsandPositionsShouldBeCorrect(string [] searchWords, Chunk chunk, List<KeyValuePair<string, int>> result)
        {
            //arrange
            Searcher searcher = new Searcher(searchWords, chunk, _intermediateQueue);
            int numDetachedTasks = 1;

            //act
            searcher.FindWordsAndPositions();

            //assert
            Assert.AreEqual(result, _intermediateQueue.GetQueue());
            Assert.AreEqual(result.Count, _intermediateQueue.Count());
            Assert.AreEqual(numDetachedTasks, _intermediateQueue.CountDetachedTasks());          

        }
    }
}
