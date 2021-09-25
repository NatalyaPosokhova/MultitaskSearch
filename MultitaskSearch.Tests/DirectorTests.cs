using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultitaskSearch.Tests
{
    public class DirectorTests
    {
        private List<Chunk> chunks;
        private AbstractDirector director;
        private int blockSize = 3;

        [SetUp]
        public void Setup() 
        {
            chunks = new List<Chunk>();
            director = Substitute.For<AbstractDirector>(chunks, new string[] { "abc"});
        }

        [Test]
        public async Task SearcherAndCollectorShouldBeCalledOneTimeBeSuccess()
        {
            //arrange
            blockSize = 5;
            chunks.Add(new Chunk() { StartIndex = 0, Content = "abc d" });

            //act
            await director.GetWordsPositions();

            //assert
            await director.Received(1).CreateCollector(Arg.Any<IntermediateQueue>());
            director.Received(1).StartSearcher(Arg.Any<string[]>(), Arg.Any<Chunk>(), Arg.Any<IntermediateQueue>());                              
        }

        [Test]
        public async Task CallSearcherTwoTimesShouldBeSuccess()
        {
            //arrange
            blockSize = 3;
            chunks.Add(new Chunk() { StartIndex = 0, Content = "qwe" });
            chunks.Add(new Chunk() { StartIndex = 3, Content = "rty" });

            //act
            await director.GetWordsPositions();

            //assert
            await director.Received(1).CreateCollector(Arg.Any<IntermediateQueue>());
            director.Received(2).StartSearcher(Arg.Any<string[]>(), Arg.Any<Chunk>(), Arg.Any<IntermediateQueue>());
        }

        [Test]
        public async Task TransferChunkToSearcherShouldBeCorrect()
        {
            //arrange
            chunks.Add(new Chunk() { StartIndex = 0, Content = "qwe" });
            chunks.Add(new Chunk() { StartIndex = 3, Content = "rty" });

            //act
            await director.GetWordsPositions();

            //assert
            director.Received().StartSearcher(Arg.Any<string[]>(), chunks[0], Arg.Any<IntermediateQueue>());
            director.Received().StartSearcher(Arg.Any<string[]>(), chunks[1], Arg.Any<IntermediateQueue>());
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