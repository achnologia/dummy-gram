using System.Threading.Tasks;
using DummyGram.Application.AppUsers.Repositories;
using DummyGram.Application.AppUsers.Services;
using DummyGram.Application.Posts.Repositories;
using DummyGram.Domain.Entities;
using Moq;
using Xunit;

namespace DummyGram.Application.UnitTests.AppUsers.Services;

public class AppUserService_Tests
{
    private readonly Mock<IAppUserRepository> _repository;
    private readonly Mock<IPostRepository> _postRepository;
    private readonly IAppUserService _service;
    
    public AppUserService_Tests()
    {
        _repository = new Mock<IAppUserRepository>();
        _postRepository = new Mock<IPostRepository>();
        _service = new AppUserService(_repository.Object, _postRepository.Object);
    }

    private AppUser GetTestUser()
    {
        return new AppUser("test");
    }

    private Post GetTestPost()
    {
        var test = "test";

        return new Post(test, test, test);
    }
    
    [Fact]
    public async Task UpdateAsync_ShouldUpdate()
    {
        // Arrange
        var id = "id";
        var displayName = "displayName";
        var mock = new Mock<AppUser>();

        _repository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(mock.Object);

        // Act
        var result = await _service.UpdateAsync(id, displayName);

        // Assert
        mock.Verify(x => x.Update(It.IsAny<string>()), Times.Once);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<AppUser>()), Times.Once);
    }
    
    [Fact]
    public async Task UpdateAsync_UserDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var id = "id";
        var displayName = "displayName";
        var mock = new Mock<AppUser>();

        _repository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync((AppUser)null);

        // Act
        var result = await _service.UpdateAsync(id, displayName);

        // Assert
        Assert.False(result);
        mock.Verify(x => x.Update(It.IsAny<string>()), Times.Never);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<AppUser>()), Times.Never);
    }

    [Fact]
    public async Task SubscribeAsync_ShouldSubscribe()
    {
        // Arrange
        var idUserSubscriber = "idUserSubscriber";
        var idUserSubscribeTo = "idUserSubscribeTo";
        var appUserSubTo = new Mock<AppUser>();
        var appUserSubscriber = GetTestUser();

        _repository.Setup(x => x.GetByIdAsync(idUserSubscribeTo)).ReturnsAsync(appUserSubTo.Object);
        _repository.Setup(x => x.GetByIdAsync(idUserSubscriber)).ReturnsAsync(appUserSubscriber);
        appUserSubTo.Setup(x => x.HasSubscriber(It.IsAny<AppUser>())).Returns(false);

        // Act
        var result = await _service.SubscribeAsync(idUserSubscriber, idUserSubscribeTo);

        // Assert
        appUserSubTo.Verify(x => x.Subscribe(It.IsAny<AppUser>()), Times.Once);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<AppUser>()), Times.Once);
    }
    
    [Fact]
    public async Task SubscribeAsync_UserSubscribeToDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var idUserSubscriber = "idUserSubscriber";
        var idUserSubscribeTo = "idUserSubscribeTo";
        var appUserSubscriber = GetTestUser();

        _repository.Setup(x => x.GetByIdAsync(idUserSubscribeTo)).ReturnsAsync((AppUser)null);
        _repository.Setup(x => x.GetByIdAsync(idUserSubscriber)).ReturnsAsync(appUserSubscriber);

        // Act
        var result = await _service.SubscribeAsync(idUserSubscriber, idUserSubscribeTo);

        // Assert
        Assert.False(result);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<AppUser>()), Times.Never);
    }
    
    [Fact]
    public async Task SubscribeAsync_UserSubscriberDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var idUserSubscriber = "idUserSubscriber";
        var idUserSubscribeTo = "idUserSubscribeTo";
        var appUserSubTo = new Mock<AppUser>();
        AppUser appUserSubscriber = null;

        _repository.Setup(x => x.GetByIdAsync(idUserSubscribeTo)).ReturnsAsync(appUserSubTo.Object);
        _repository.Setup(x => x.GetByIdAsync(idUserSubscriber)).ReturnsAsync(appUserSubscriber);
        appUserSubTo.Setup(x => x.HasSubscriber(It.IsAny<AppUser>())).Returns(false);

        // Act
        var result = await _service.SubscribeAsync(idUserSubscriber, idUserSubscribeTo);

        // Assert
        Assert.False(result);
        appUserSubTo.Verify(x => x.Subscribe(It.IsAny<AppUser>()), Times.Never);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<AppUser>()), Times.Never);
    }
    
    [Fact]
    public async Task SubscribeAsync_UserAlreadySubscribed_ShouldReturnFalse()
    {
        var idUserSubscriber = "idUserSubscriber";
        var idUserSubscribeTo = "idUserSubscribeTo";
        var appUserSubTo = new Mock<AppUser>();
        var appUserSubscriber = GetTestUser();

        _repository.Setup(x => x.GetByIdAsync(idUserSubscribeTo)).ReturnsAsync(appUserSubTo.Object);
        _repository.Setup(x => x.GetByIdAsync(idUserSubscriber)).ReturnsAsync(appUserSubscriber);
        appUserSubTo.Setup(x => x.HasSubscriber(It.IsAny<AppUser>())).Returns(true);

        // Act
        var result = await _service.SubscribeAsync(idUserSubscriber, idUserSubscribeTo);

        // Assert
        Assert.False(result);
        appUserSubTo.Verify(x => x.Subscribe(It.IsAny<AppUser>()), Times.Never);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<AppUser>()), Times.Never);
    }
    
    [Fact]
    public async Task UnsubscribeAsync_ShouldUnsubscribe()
    {
        // Arrange
        var idUserSubscriber = "idUserSubscriber";
        var idUserSubscribeTo = "idUserSubscribeTo";
        var appUserSubTo = new Mock<AppUser>();
        var appUserSubscriber = GetTestUser();

        _repository.Setup(x => x.GetByIdAsync(idUserSubscribeTo)).ReturnsAsync(appUserSubTo.Object);
        _repository.Setup(x => x.GetByIdAsync(idUserSubscriber)).ReturnsAsync(appUserSubscriber);

        // Act
        var result = await _service.UnsubscribeAsync(idUserSubscriber, idUserSubscribeTo);

        // Assert
        appUserSubTo.Verify(x => x.Unsubscribe(It.IsAny<AppUser>()), Times.Once);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<AppUser>()), Times.Once);
    }
    
    [Fact]
    public async Task UnsubscribeAsync_UserSubscribeToDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var idUserSubscriber = "idUserSubscriber";
        var idUserSubscribeTo = "idUserSubscribeTo";
        var appUserSubscriber = GetTestUser();

        _repository.Setup(x => x.GetByIdAsync(idUserSubscribeTo)).ReturnsAsync((AppUser)null);
        _repository.Setup(x => x.GetByIdAsync(idUserSubscriber)).ReturnsAsync(appUserSubscriber);

        // Act
        var result = await _service.UnsubscribeAsync(idUserSubscriber, idUserSubscribeTo);

        // Assert
        Assert.False(result);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<AppUser>()), Times.Never);
    }
    
    [Fact]
    public async Task UnsubscribeAsync_UserSubscriberDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var idUserSubscriber = "idUserSubscriber";
        var idUserSubscribeTo = "idUserSubscribeTo";
        var appUserSubTo = new Mock<AppUser>();
        AppUser appUserSubscriber = null;

        _repository.Setup(x => x.GetByIdAsync(idUserSubscribeTo)).ReturnsAsync(appUserSubTo.Object);
        _repository.Setup(x => x.GetByIdAsync(idUserSubscriber)).ReturnsAsync(appUserSubscriber);
        appUserSubTo.Setup(x => x.HasSubscriber(It.IsAny<AppUser>())).Returns(false);

        // Act
        var result = await _service.UnsubscribeAsync(idUserSubscriber, idUserSubscribeTo);

        // Assert
        Assert.False(result);
        appUserSubTo.Verify(x => x.Unsubscribe(It.IsAny<AppUser>()), Times.Never);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<AppUser>()), Times.Never);
    }
    
    [Fact]
    public async Task SavePostAsync_ShouldSavePost()
    {
        // Arrange
        var id = "idUser";
        var idPost = 1;
        var appUser = new Mock<AppUser>();
        var post = GetTestPost();

        _repository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(appUser.Object);
        _postRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(post);

        // Act
        var result = await _service.SavePostAsync(id, idPost);

        // Assert
        appUser.Verify(x => x.SavePost(It.IsAny<Post>()), Times.Once);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<AppUser>()), Times.Once);
    }
    
    [Fact]
    public async Task SavePostAsync_UserDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var id = "idUser";
        var idPost = 1;
        AppUser appUser = null;
        var post = GetTestPost();

        _repository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(appUser);
        _postRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(post);

        // Act
        var result = await _service.SavePostAsync(id, idPost);

        // Assert
        Assert.False(result);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<AppUser>()), Times.Never);
    }
    
    [Fact]
    public async Task SavePostAsync_PostDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var id = "idUser";
        var idPost = 1;
        var appUser = new Mock<AppUser>();
        Post post = null;

        _repository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(appUser.Object);
        _postRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(post);

        // Act
        var result = await _service.SavePostAsync(id, idPost);

        // Assert
        Assert.False(result);
        appUser.Verify(x => x.SavePost(It.IsAny<Post>()), Times.Never);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<AppUser>()), Times.Never);
    }
    
    [Fact]
    public async Task RemoveSavedPostAsync_ShouldRemoveSavedPost()
    {
        // Arrange
        var id = "idUser";
        var idPost = 1;
        var appUser = new Mock<AppUser>();
        var post = GetTestPost();

        _repository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(appUser.Object);
        _postRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(post);

        // Act
        var result = await _service.RemoveSavedPostAsync(id, idPost);

        // Assert
        appUser.Verify(x => x.RemoveSavedPost(It.IsAny<Post>()), Times.Once);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<AppUser>()), Times.Once);
    }
    
    [Fact]
    public async Task RemoveSavedPostAsync_UserDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var id = "idUser";
        var idPost = 1;
        AppUser appUser = null;
        var post = GetTestPost();

        _repository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(appUser);
        _postRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(post);

        // Act
        var result = await _service.RemoveSavedPostAsync(id, idPost);

        // Assert
        Assert.False(result);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<AppUser>()), Times.Never);
    }
    
    [Fact]
    public async Task RemoveSavedPostAsync_PostDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var id = "idUser";
        var idPost = 1;
        var appUser = new Mock<AppUser>();
        Post post = null;

        _repository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(appUser.Object);
        _postRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(post);

        // Act
        var result = await _service.RemoveSavedPostAsync(id, idPost);

        // Assert
        Assert.False(result);
        appUser.Verify(x => x.RemoveSavedPost(It.IsAny<Post>()), Times.Never);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<AppUser>()), Times.Never);
    }
}