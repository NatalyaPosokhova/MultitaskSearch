using NUnit.Framework;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MultitaskSearch.Tests
{
    public class IntegrationTests
    {
        [Test]
        public void ReadFileAndSearchWordsShouldBeSuccess()
        {
            //arrange
            FileProvider provider = new FileProvider("book.txt", 20);
            Director director = new Director(provider, new string[] { "Смоленска", "армия", "французы", "свои", "тест"});
            Dictionary<string, IList<int>> expected = new Dictionary<string, IList<int>> 
            {
                { "Смоленска", new List<int>{ 801, 815, 4602 } },
                { "армия", new List<int>{ 721 }},
                { "французы", new List<int>{ 2667 } },
                { "свои", new List<int>{ 2717, 2947 }}     
            };

            //act
            var actual = director.GetWordsPositions().Result;

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
