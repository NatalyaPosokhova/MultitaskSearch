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
        private int blockSize;

        [SetUp]
        public void Setup() 
        {
            provider = Substitute.For<IDataProvider>();
            director = Substitute.For<AbstractDirector>(provider, blockSize, new char[] { 'a', 'b'});
        }

        [Test]
        public async void SearcherAndCollectorShouldBeCalledOneTimeBeSuccess()
        {
            //arrange
            blockSize = 5;
            var chunk = new Chunk() { StartIndex = 0, Content = new char[] { 'a', 'b', 'c', ' ', 'd' } };

            provider.GetData(blockSize).Returns(chunk);
            provider.IsDataExists().Returns( true, false );

            //act
            await director.GetWordsPositions();

            //assert
            await director.Received(1).CreateCollector();
            director.Received(1).CreateSearcher(Arg.Any<Chunk>());                              
        }

        [Test]
        public async void CallSearcherTwoTimesShouldBeSuccess()
        {
            //arrange
            blockSize = 3;
            var chunk1 = new Chunk() { StartIndex = 0, Content = new char[] { 'q', 'w', 'e' } };
            var chunk2 = new Chunk() { StartIndex = 3, Content = new char[] { 'r', 't', 'y' } };

            provider.GetData(blockSize).Returns(chunk1, chunk2);
            provider.IsDataExists().Returns(true, true, false);

            //act
            await director.GetWordsPositions();

            //assert
            await director.Received(1).CreateCollector();
            director.Received(2).CreateSearcher(Arg.Any<Chunk>());
        }

        [Test]
        public async void TransferChunkToSearcherShouldBeCorrect()
        {
            //arrange
            blockSize = 3;
            var chunk1 = new Chunk() { StartIndex = 0, Content = new char[] { 'q', 'w', 'e' } };
            var chunk2 = new Chunk() { StartIndex = 3, Content = new char[] { 'r', 't', 'y' } };

            provider.GetData(blockSize).Returns(chunk1, chunk2);
            provider.IsDataExists().Returns(true, true, false);

            Chunk[] actuals = new Chunk[2];

            director.When(x => x.CreateSearcher(Arg.Is(chunk1))).Do(x =>actuals[0] =  x.ArgAt<Chunk>(0));
            director.When(x => x.CreateSearcher(Arg.Is(chunk2))).Do(x => actuals[1] = x.ArgAt<Chunk>(0));

            //act
            await director.GetWordsPositions();

            //assert
            Assert.AreEqual(chunk1, actuals[0]);
            Assert.AreEqual(chunk2, actuals[1]);
        }

        [Test]
        public void CallWordspositionsShouldBeCorrect()
        {
            //arrange
            blockSize = 4;
            var chunk1 = new Chunk() { StartIndex = 0, Content = new char[] { 'a', 'b', 'c', 'a' } };
            var chunk2 = new Chunk() { StartIndex = 3, Content = new char[] { 'd', 'e', 'e', 'a' } };

            provider.GetData(blockSize).Returns(chunk1, chunk2);
            provider.IsDataExists().Returns(true, true, false);

            Dictionary<string, List<int>> expected = new Dictionary<string, List<int>> {
                { "a", new List<int> { 1, 4, 8 }},
                { "b", new List<int> { 2 }}
            };

            //act
            var actual = director.GetWordsPositions();

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}