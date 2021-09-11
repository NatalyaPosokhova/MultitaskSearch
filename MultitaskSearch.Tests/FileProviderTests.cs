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

            CreateTestData(fileData);
            FileProvider fileProvider = new FileProvider(filePath, dataSize);

            //act
            List<Chunk> actualChunks = new List<Chunk>();
            do
            {
                actualChunks.Add(fileProvider.Current);

            } while (fileProvider.MoveNext());

            //assert
            Assert.AreEqual(3, actualChunks.Count);
            Assert.AreEqual(expectedContent1, actualChunks[0].Content);
            Assert.AreEqual(expectedStartIndex1, actualChunks[0].StartIndex);
            Assert.AreEqual(expectedContent2, actualChunks[1].Content);
            Assert.AreEqual(expectedStartIndex2, actualChunks[1].StartIndex);
            Assert.AreEqual(expectedContent3, actualChunks[2].Content);
            Assert.AreEqual(expectedStartIndex3, actualChunks[2].StartIndex);
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
