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
                    new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("abc", 5) })));

            yield return new TestCaseData(new string[] { "abc", "def" }, new Chunk() { StartIndex = 0, Content = "abc tyu" },
                new IntermediateQueue(new ConcurrentQueue<KeyValuePair<string, int>>(
                    new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("abc", 0) })));

            yield return new TestCaseData(new string[] { "abc", "def" }, new Chunk() { StartIndex = 14, Content = " fds def tyu abc" },
                new IntermediateQueue(new ConcurrentQueue<KeyValuePair<string, int>>(
                    new List<KeyValuePair<string, int>>() {new KeyValuePair<string, int>("def", 19),
                        new KeyValuePair<string, int>("abc", 27) })));

            yield return new TestCaseData(new string[] { "abc", "def" }, new Chunk() { StartIndex = 0, Content = " fds deft tyuabc" },
                new IntermediateQueue(new ConcurrentQueue<KeyValuePair<string, int>>()));

            yield return new TestCaseData(new string[] { "abc", "def" }, new Chunk() { StartIndex = 0, Content = " fds deft tyuabc" },
                new IntermediateQueue(new ConcurrentQueue<KeyValuePair<string, int>>()));

            yield return new TestCaseData(new string[] { "abc", "def" }, new Chunk() { StartIndex = 0, Content = "" },
                new IntermediateQueue(new ConcurrentQueue<KeyValuePair<string, int>>()));
            
            yield return new TestCaseData(new string[] { "abc", "def" }, new Chunk() { StartIndex = 0, Content = "de abc, tra abc" },
                new IntermediateQueue(new ConcurrentQueue<KeyValuePair<string, int>>(
                    new List<KeyValuePair<string, int>>() {new KeyValuePair<string, int>("abc", 3),
                        new KeyValuePair<string, int>("abc", 12) })));

            yield return new TestCaseData(new string[] { "abc", "def" }, new Chunk() { StartIndex = 0, Content = "de abc tra abc." },
            new IntermediateQueue(new ConcurrentQueue<KeyValuePair<string, int>>(
                new List<KeyValuePair<string, int>>() {new KeyValuePair<string, int>("abc", 3),
                                new KeyValuePair<string, int>("abc", 11) })));

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

        [Test]
        public void CheckSearcherWasDetached()
        {
            //arrange
            string[] searchWords = new string[] { "abc", "def" };
            var chunk = new Chunk() { StartIndex = 0, Content = "abc ref" };
            Searcher searcher = new Searcher(searchWords, chunk, _intermediateQueue);
            int expected = 1;
            //act
            searcher.FindWordsAndPositions();

            //assert
            Assert.AreEqual(expected, _intermediateQueue.CountDetachedTasks());
        }
    }
}
