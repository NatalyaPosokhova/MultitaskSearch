using NUnit.Framework;
using NSubstitute;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

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

       

       
    }
}
