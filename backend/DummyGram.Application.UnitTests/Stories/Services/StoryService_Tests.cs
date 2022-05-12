using System.Threading.Tasks;
using DummyGram.Application.Stories.Repositories;
using DummyGram.Application.Stories.Services;
using DummyGram.Domain.Entities;
using Moq;
using Xunit;

namespace DummyGram.Application.UnitTests.Stories.Services;

public class StoryService_Tests
{
    private readonly Mock<IStoryRepository> _repository;
    private readonly IStoryService _service;

    public StoryService_Tests()
    {
        _repository = new Mock<IStoryRepository>();
        _service = new StoryService(_repository.Object);
    }

    private Story GetTestStory()
    {
        var test = "test";

        return new Story(test, test);
    }
    
    [Fact]
    public async Task CreateAsync_ShouldCreate()
    {
        // Arrange
        var idUser = "idUser";
        var imageUrl = "imageUrl";

        // Act
        await _service.CreateAsync(idUser, imageUrl);

        // Assert
        _repository.Verify(x => x.AddAsync(It.IsAny<Story>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDelete()
    {
        // Arrange
        var id = 1;

        // Act
        var result = await _service.DeleteAsync(id);

        // Assert
        _repository.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task IsUserStoryAuthor_UserIsAuthor_ShouldReturnTrue()
    {
        // Arrange
        var id = 1;
        var idUser = "test";
        var story = GetTestStory();

        _repository.Setup(x => x.GetByIdNoTrackingAsync(It.IsAny<int>())).ReturnsAsync(story);

        // Act
        var result = await _service.IsUserStoryAuthor(id, idUser);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public async Task IsUserStoryAuthor_UserIsNotAuthor_ShouldReturnFalse()
    {
        // Arrange
        var id = 1;
        var idUser = "idUser";
        var story = GetTestStory();

        _repository.Setup(x => x.GetByIdNoTrackingAsync(It.IsAny<int>())).ReturnsAsync(story);

        // Act
        var result = await _service.IsUserStoryAuthor(id, idUser);

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public async Task IsUserStoryAuthor_StoryDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var id = 1;
        var idUser = "test";
        Story story = null;

        _repository.Setup(x => x.GetByIdNoTrackingAsync(It.IsAny<int>())).ReturnsAsync(story);

        // Act
        var result = await _service.IsUserStoryAuthor(id, idUser);

        // Assert
        Assert.False(result);
    }
}