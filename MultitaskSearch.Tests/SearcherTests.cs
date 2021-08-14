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
        static IEnumerable<TestCaseData> GetSeacherData()
        {
            yield return new TestCaseData(new string[] { "abc" }, new Chunk() { StartIndex = 0, Content = " fds abc tyu" },
                new IntermediateQueue(new ConcurrentQueue<KeyValuePair<string, int>>(
                    new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("abc", 6) })));

            yield return new TestCaseData(new string[] { "abc, def" }, new Chunk() { StartIndex = 0, Content = "abc tyu" },
                new IntermediateQueue(new ConcurrentQueue<KeyValuePair<string, int>>(
                    new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("abc", 1) })));

            yield return new TestCaseData(new string[] { "abc, def" }, new Chunk() { StartIndex = 14, Content = " fds def tyu abc" },
                new IntermediateQueue(new ConcurrentQueue<KeyValuePair<string, int>>(
                    new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("abc", 28), 
                    new KeyValuePair<string, int>("def", 6) })));

            yield return new TestCaseData(new string[] { "abc, def" }, new Chunk() { StartIndex = 0, Content = " fds deft tyuabc" },
                new IntermediateQueue(new ConcurrentQueue<KeyValuePair<string, int>>()));

            yield return new TestCaseData(new string[] { "abc, def" }, new Chunk() { StartIndex = 0, Content = " fds deft tyuabc" },
                new IntermediateQueue(new ConcurrentQueue<KeyValuePair<string, int>>()));

            yield return new TestCaseData(new string[] { "abc, def" }, new Chunk() { StartIndex = 0, Content = "" },
                new IntermediateQueue(new ConcurrentQueue<KeyValuePair<string, int>>()));

        }
        [Test]
        [TestCaseSource("GetSeacherData")]
        public void FindWordsandPositionsShouldBeCorrect(string [] searchWords, Chunk chunk, IntermediateQueue expected)
        {
            //arrange
            Searcher searcher = new Searcher(searchWords, chunk, _intermediateQueue);

            //act
            searcher.FindWordsAndPositions();

            //assert
            Assert.AreEqual(expected, _intermediateQueue);
        }
    }
}
