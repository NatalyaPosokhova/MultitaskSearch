using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultitaskSearch.Tests
{
    public class DirectorTest
    {
        private IDataProvider provider;
        private AbstractDirector director;

        [SetUp]
        public void Setup()
        {
            provider = Substitute.For<IDataProvider>();
            director = Substitute.For<AbstractDirector>(provider);
        }

        [Test]
        public void SearcherAndCollectorShouldBeCalledOneTimeBeSuccess()
        {
            //arrange
            var chunk = new Chunk() { StartIndex = 0, Content = new char[] { 'a', 'b', 'c', ' ', 'd' } };

            provider.GetData(5).Returns(chunk);
            provider.IsDataExists().Returns( true, false );
            
            //Dictionary<string, int[]> expected = new Dictionary<string, int[]>() { };
            //act
            director.GetWordsPositions();

            //assert
            director.Received(1).CreateSearcher(Arg.Any<Chunk>());
            director.Received(1).CreateCollector();                    
        }

        [Test]
        public void CallSearcherTwoTimesShouldBeSuccess()
        {
            //arrange
            var chunk1 = new Chunk() { StartIndex = 0, Content = new char[] { 'q', 'w', 'e' } };
            var chunk2 = new Chunk() { StartIndex = 3, Content = new char[] { 'r', 't', 'y' } };

            provider.GetData(3).Returns(chunk1, chunk2);
            provider.IsDataExists().Returns(true, true, false);

            //act
            director.GetWordsPositions();

            //assert
            director.Received(2).CreateSearcher(Arg.Any<Chunk>());
            director.Received(1).CreateCollector();
        }

        [Test]
        public void TransferChunkToSearcherShouldBeCorrect()
        {
            //arrange
            var chunk1 = new Chunk() { StartIndex = 0, Content = new char[] { 'q', 'w', 'e' } };
            var chunk2 = new Chunk() { StartIndex = 3, Content = new char[] { 'r', 't', 'y' } };

            provider.GetData(3).Returns(chunk1, chunk2);
            provider.IsDataExists().Returns(true, true, false);

            Chunk[] actuals = new Chunk[2];

            director.When(x => x.CreateSearcher(Arg.Is(chunk1))).Do(x =>actuals[0] =  x.ArgAt<Chunk>(0));
            director.When(x => x.CreateSearcher(Arg.Is(chunk2))).Do(x => actuals[1] = x.ArgAt<Chunk>(0));
            //act
            director.GetWordsPositions();

            //assert
            Assert.AreEqual(chunk1, actuals[0]);
            Assert.AreEqual(chunk2, actuals[1]);
        }

        [Test]
        public void CallWordspositionsShouldBeCorrect()
        {
            //TODO
        }
    }
}