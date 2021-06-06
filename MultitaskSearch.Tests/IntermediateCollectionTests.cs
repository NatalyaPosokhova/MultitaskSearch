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
        private IDataProvider provider;
        private AbstractDirector director;

        [SetUp]
        public void Setup()
        {
            provider = Substitute.For<IDataProvider>();
            director = Substitute.For<AbstractDirector>(provider, 5, new string[] { "abc" });
            intermediateCollection = new IntermediateCollection();
        }

        [Test]
        public async Task InsertWordAndPositionToQueueShouldBeCorrect()
        {
            //arrange
            var chunk = new Chunk() { StartIndex = 0, Content = "abc d abc" };

            provider.GetData(9).Returns(chunk);
            provider.IsDataExists().Returns(true, false);

            director.CreateSearcher(chunk, intermediateCollection);
            //director.When(x => x.CreateSearcher(chunk, intermediateCollection)).Do();
            await director.GetWordsPositions();

            //act
            var dictionaryToInsert = await intermediateCollection.GetSearchResults();
            //intermediateCollection.InsertDataToQueue(actSearchResults.First());

            //assert

        }

        [Test]
        public async Task GetSearchResultAndDeleteShouldBeSuccess()
        {
            //arrange
            var chunk1 = new Chunk() { StartIndex = 0, Content = "der abc test" };
            var chunk2 = new Chunk() { StartIndex = 0, Content = "test abc    " };
            provider.GetData(12).Returns(chunk1, chunk2);
            provider.IsDataExists().Returns(true, true, false);

            director.CreateSearcher(chunk1, intermediateCollection);
            director.CreateSearcher(chunk2, intermediateCollection);

            await director.GetWordsPositions();

            //act
            Dictionary<string, IList<int>> actSearchResults = await intermediateCollection.GetSearchResults();
            //intermediateCollection.InsertDataToQueue(actSearchResults.First());

            //Чтобы получить реальный остаток в объекте сёчера, возможно надо будет в коассе Searcher 
            //реализовать метод GetSearchResults()
            //var searcherRemainder = 

            //assert

        }

        [Test]
        public async Task InformAboutEndWorkWithQueueShouldBeSuccess()
        {
            //arrange
            var chunk = new Chunk() { StartIndex = 0, Content = "a b c de" };
            provider.GetData(8).Returns(chunk);
            provider.IsDataExists().Returns(true, false);

            director.CreateSearcher(chunk, intermediateCollection);

            await director.GetWordsPositions();
            //act
            //Насколько я поняла, Searcher должен уведомить intermediateCollection о том, что он последний,
            //а intermediateCollection должна уведомить collector

            //assert
        }

        [Test]
        public async Task GetInfoAboutNumberEndCallingShouldBeCorrect()
        {
            //arrange
            var chunk = new Chunk() { StartIndex = 0, Content = "dessert fruits" };
            provider.GetData(14).Returns(chunk);
            provider.IsDataExists().Returns(true, false);

            director.CreateSearcher(chunk, intermediateCollection);

            await director.GetWordsPositions();

            //act

            //assert
        }
    }
}
