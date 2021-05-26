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
        private int blockSize = 3;

        [SetUp]
        public void Setup() 
        {
            provider = Substitute.For<IDataProvider>();
            director = Substitute.For<AbstractDirector>(provider, blockSize, new string[] { "abc"});
        }

        [Test]
        public async Task SearcherAndCollectorShouldBeCalledOneTimeBeSuccess()
        {
            //arrange
            blockSize = 5;
            var chunk = new Chunk() { StartIndex = 0, Content = "abc d" };

            provider.GetData(blockSize).Returns(chunk);
            provider.IsDataExists().Returns( true, false );

            //act
            await director.GetWordsPositions();

            //assert
            await director.Received(1).CreateCollector(Arg.Any<IntermediateCollection>());
            director.Received(1).CreateSearcher(Arg.Any<Chunk>(), Arg.Any<IntermediateCollection>());                              
        }

        [Test]
        public async Task CallSearcherTwoTimesShouldBeSuccess()
        {
            //arrange
            blockSize = 3;
            var chunk1 = new Chunk() { StartIndex = 0, Content = "qwe" };
            var chunk2 = new Chunk() { StartIndex = 3, Content = "rty" };

            provider.GetData(blockSize).Returns(chunk1, chunk2);
            provider.IsDataExists().Returns(true, true, false);

            //act
            await director.GetWordsPositions();

            //assert
            await director.Received(1).CreateCollector(Arg.Any<IntermediateCollection>());
            director.Received(2).CreateSearcher(Arg.Any<Chunk>(), Arg.Any<IntermediateCollection>());
        }

        [Test]
        public async Task TransferChunkToSearcherShouldBeCorrect()
        {
            //arrange
            var chunk1 = new Chunk() { StartIndex = 0, Content = "qwe" };
            var chunk2 = new Chunk() { StartIndex = 3, Content = "rty" };

            provider.GetData(blockSize).Returns(chunk1, chunk2);
            provider.IsDataExists().Returns(true, true, false);

            Chunk[] actuals = new Chunk[2];

            director.When(x => x.CreateSearcher(Arg.Is(chunk1), Arg.Any<IntermediateCollection>())).Do(x =>actuals[0] =  x.ArgAt<Chunk>(0));
            director.When(x => x.CreateSearcher(Arg.Is(chunk2), Arg.Any<IntermediateCollection>())).Do(x => actuals[1] = x.ArgAt<Chunk>(0));

            //act
            await director.GetWordsPositions();

            //assert
            Assert.AreEqual(chunk1, actuals[0]);
            Assert.AreEqual(chunk2, actuals[1]);
        }

        //[Test]
        //public async Task CallWordspositionsShouldBeCorrect()
        //{
        //    //arrange
        //    AbstractDirector director_ = new Director(provider, blockSize, new string[] { "abc", "a", "tr" });

        //    blockSize = 4;
        //    var chunk1 = new Chunk() { StartIndex = 0, Content = "abc a" };
        //    var chunk2 = new Chunk() { StartIndex = 3, Content = "deea tr" };

        //    provider.GetData(blockSize).Returns(chunk1, chunk2);
        //    provider.IsDataExists().Returns(true, true, false);

        //    Dictionary<string, List<int>> expected = new Dictionary<string, List<int>> {
        //        { "abc", new List<int> { 1} },
        //        { "a", new List<int> { 5 }},
        //        { "tr", new List<int> { 11 }}
        //    };

        //    //act
        //    var actual = await director_.GetWordsPositions();

        //    //assert
        //    Assert.AreEqual(expected, actual);
        //}
    }
}