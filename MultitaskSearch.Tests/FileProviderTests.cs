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
            Chunk expectedChunk = new Chunk() { Content = expectedContent, StartIndex = expectedStartIndex };

            CreateTestData(expectedContent);
            FileProvider fileProvider = new FileProvider(filePath, dataSize);

            //act
            var iterator = fileProvider.GetEnumerator();
            iterator.MoveNext();
            Chunk actualChunk = iterator.Current;

            //assert
            Assert.AreEqual(expectedChunk, actualChunk);
        }

        [Test]
        public void ReadDataToThreeChunksShouldBeSuccess()
        {
            //arrange
            string fileData = "Lorem ipsum dolor sit test";
            int dataSize = 9;
            string expectedContent1 = "Lorem ipsum ";
            string expectedContent2 = "dolor sit ";
            string expectedContent3 = "test";

            int expectedStartIndex1= 0;
            int expectedStartIndex2 = 12;
            int expectedStartIndex3 = 22;


            List<Chunk> expectedChunks = new List<Chunk>
            {
                new Chunk { StartIndex = expectedStartIndex1, Content = expectedContent1 },
                new Chunk { StartIndex = expectedStartIndex2, Content = expectedContent2 },
                new Chunk { StartIndex = expectedStartIndex3, Content = expectedContent3 }
            };

            CreateTestData(fileData);
            FileProvider fileProvider = new FileProvider(filePath, dataSize);

            //act
            List<Chunk> actualChunks = new List<Chunk>();
            var iterator = fileProvider.GetEnumerator();
            iterator.MoveNext();
            actualChunks.Add(iterator.Current);
            iterator.MoveNext();
            actualChunks.Add(iterator.Current);
            iterator.MoveNext();
            actualChunks.Add(iterator.Current);


            //assert
            Assert.AreEqual(expectedChunks, actualChunks);
        }

        [Test]
        public void TryReadDataWithTwiSpaces()
        {
            string fileData = "test  data  common";
            int dataSize = 6;
            List<Chunk> expectedChunks = new List<Chunk>() { 
                new Chunk { Content = "test", StartIndex = 0 },
                new Chunk { Content = "data", StartIndex = 6 },

            };
        }

        [Test]
        public void TryReadDataFromUnexistedFileShouldBeException()
        {
            //arrange
            int dataSize = 9;
            FileProvider fileProvider = new FileProvider(filePath, dataSize);
            //act
            //assert
            Assert.Throws<FileNotFoundException>(() => fileProvider.GetEnumerator().MoveNext());
        }

        private void CreateTestData(string source)
        {
            File.WriteAllText(filePath, source);
        }
    }
}
