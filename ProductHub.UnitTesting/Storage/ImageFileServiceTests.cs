using Microsoft.Extensions.Configuration;
using Moq;
using ProductHub.Storage.Services;
using Xunit;

namespace ProductHub.UnitTesting.Storage
{
    public class ImageFileServiceTests
    {
        [Fact]
        public async Task Upload_ValidFile_ShouldReturnSuccess()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x["ImagesPath"]).Returns("test");

            var service = new ImageFileService(configurationMock.Object);

            var fileName = "test.jpg";
            var fileStream = new MemoryStream(new byte[] { 0x00, 0x01, 0x02 }); // Example file content

            // Act
            var result = await service.Upload(fileName, fileStream);

            // Assert
            Assert.True(result.ProcessedSuccessfully);
            Assert.NotEmpty(result.FilePath);

            // Cleanup
            await service.Delete(result.FilePath);
        }

        [Fact]
        public async Task Upload_EmptyFileStream_ShouldReturnError()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x["ImagesPath"]).Returns("test");

            var service = new ImageFileService(configurationMock.Object);

            var fileName = "test.jpg";
            Stream emptyStream = new MemoryStream();

            // Act
            var result = await service.Upload(fileName, emptyStream);

            // Assert
            Assert.False(result.ProcessedSuccessfully);
            Assert.Contains($"FileStream or FileName is empty: {fileName}", result.Message);
        }

        [Fact]
        public async Task Delete_ExistingFile_ShouldReturnSuccess()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x["ImagesPath"]).Returns("test");

            var service = new ImageFileService(configurationMock.Object);

            var fileName = "test.jpg";
            var fileStream = new MemoryStream(new byte[] { 0x00, 0x01, 0x02 });

            var uploadResult = await service.Upload(fileName, fileStream);

            // Act
            var deleteResult = await service.Delete(uploadResult.FilePath);

            // Assert
            Assert.True(deleteResult.ProcessedSuccessfully);
        }

        [Fact]
        public async Task Delete_NonExistingFile_ShouldReturnError()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x["ImagesPath"]).Returns("test");

            var service = new ImageFileService(configurationMock.Object);

            var nonExistingFilePath = "non-existing-file.jpg";

            // Act
            var result = await service.Delete(nonExistingFilePath);

            // Assert
            Assert.False(result.ProcessedSuccessfully);
            Assert.Contains($"File not found: {nonExistingFilePath}", result.Message);
        }
    }
}
