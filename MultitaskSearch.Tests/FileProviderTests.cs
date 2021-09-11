using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NSubstitute;
using NUnit.Framework;

namespace MultitaskSearch.Tests
{
    public class FileProviderTests
    {//Написать приватный метод, который создаёт временный файл, в конце удалить
        string filePath = "../../../testProvider.txt";
        [TearDown]
        public void CleanUp()
        {
            File.Delete(filePath);
        }
        [Test]
        public void ReadSimpleDataShouldBeSuccess()
        {
            //arrange
            string expectedContent = "testad asdasdasdsa asdasdasd";
            int expectedStartIndex = 0;
            int dataSize = 30;

            CreateTestData(expectedContent);
            FileProvider fileProvider = new FileProvider(filePath, dataSize);

            //act
            List<Chunk> actualChunks = new List<Chunk>();
            do
            {
                actualChunks.Add(fileProvider.Current);

            } while (fileProvider.MoveNext());

            //assert
            Assert.AreEqual(expectedContent, actualChunks[0].Content);
            Assert.AreEqual(expectedStartIndex, actualChunks[0].StartIndex);
        }

        private void CreateTestData(string source)
        {
            File.WriteAllText(filePath, source);
        }
    }
}
