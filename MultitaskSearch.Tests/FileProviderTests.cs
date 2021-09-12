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
        private readonly string filePath = "../../../testProvider.txt";

        [TearDown]
        public void CleanUp()
        {
            File.Delete(filePath);
        }
        [Test]
        public void ReadSimpleDataShouldBeSuccess()
        {
            //arrange
            string fileData = "testad asdasdasdsa asdasdasd";
            string expectedContent = fileData;
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

        [Test]
        public void ReadDataToThreeChunksShouldBeSuccess()
        {
            //arrange
            string fileData = "Lorem ipsum dolor sit";
            int dataSize = 9;
            string expectedContent1 = "Lorem ";
            string expectedContent2 = "ipsum ";
            string expectedContent3 = "dolor sit";
            int expectedStartIndex1= 0;
            int expectedStartIndex2 = 6;
            int expectedStartIndex3 = 12;

            Chunk[] expectedChunks = new Chunk[]
            {
                new Chunk { StartIndex = expectedStartIndex1, Content = expectedContent1 },
                new Chunk { StartIndex = expectedStartIndex2, Content = expectedContent2 },
                new Chunk { StartIndex = expectedStartIndex3, Content = expectedContent3 }
            };

            CreateTestData(fileData);
            FileProvider fileProvider = new FileProvider(filePath, dataSize);

            //act
            List<Chunk> actualChunks = new List<Chunk>();
            do
            {
                actualChunks.Add(fileProvider.Current);

            } while (fileProvider.MoveNext());

            //assert
            Assert.AreEqual(expectedChunks, actualChunks);
        }

        [Test]
        public void TryReadDataFromUnexistedFileShouldBeException()
        {
            //arrange
            int dataSize = 9;
            FileProvider fileProvider = new FileProvider(filePath, dataSize);
            //act
            //assert
            Assert.Throws<FileNotFoundException>(() => fileProvider.GetEnumerator());
        }

        private void CreateTestData(string source)
        {
            File.WriteAllText(filePath, source);
        }
    }
}
