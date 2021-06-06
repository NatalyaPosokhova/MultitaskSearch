using NUnit.Framework;
using NSubstitute;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace MultitaskSearch.Tests
{
    public class IntermediateCollectionTests
    {
        private IntermediateCollection intermediateCollection;

        [SetUp]
        public void Setup()
        {
            intermediateCollection = new IntermediateCollection();
        }

        [Test]
        public void InsertWordAndPositionToQueueShouldBeCorrect()
        {
            //arrange            
            intermediateCollection.Put("abcd", 25);
            KeyValuePair<string, int> expected = new KeyValuePair<string, int>("abcd", 25);

            //act
            KeyValuePair<string, int> actual = intermediateCollection.Get();

            //assert
            Assert.AreEqual(expected, actual);

        }
       
    }
}
