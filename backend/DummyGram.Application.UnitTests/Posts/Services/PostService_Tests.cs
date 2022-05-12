using System.Threading.Tasks;
using DummyGram.Application.Posts.Repositories;
using DummyGram.Application.Posts.Services;
using DummyGram.Domain.Entities;
using Moq;
using Xunit;

namespace DummyGram.Application.UnitTests.Posts.Services;

public class PostService_Tests
{
    private readonly Mock<IPostRepository> _repository;
    private readonly IPostService _service;

    public PostService_Tests()
    {
        _repository = new Mock<IPostRepository>();
        _service = new PostService(_repository.Object);
    }

    private Post GetTestPost()
    {
        var test = "test";

        return new Post(test, test, test);
    }
    
    [Fact]
    public async Task GetPost_ShouldReturnPost()
    {
        // Arrange
        var id = 1;
        var post = GetTestPost();

        _repository.Setup(x => x.GetByIdNoTrackingAsync(It.IsAny<int>())).ReturnsAsync(post);

        // Act
        var result = await _service.GetPost(id);

        // Assert
        Assert.Equal(post, result);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreate()
    {
        // Arrange
        var idUser = "idUser";
        var imageUrl = "imageUrl";
        var description = "description";

        // Act
        var result = await _service.CreateAsync(idUser, imageUrl, description);

        // Assert
        _repository.Verify(x => x.AddAsync(It.IsAny<Post>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_PostExists_ShouldUpdate()
    {
        // Arrange
        var id = 1;
        var imageUrl = "imageUrl";
        var description = "description";
        var mock = new Mock<Post>();

        _repository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(mock.Object);

        // Act
        var result = await _service.UpdateAsync(id, imageUrl, description);

        // Assert
        mock.Verify(x => x.Update(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<Post>()), Times.Once);
    }
    
    [Fact]
    public async Task UpdateAsync_PostDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var id = 1;
        var imageUrl = "imageUrl";
        var description = "description";
        var mock = new Mock<Post>();

        _repository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Post)null);
        
        // Act
        var result = await _service.UpdateAsync(id, imageUrl, description);

        // Assert
        Assert.False(result);
        mock.Verify(x => x.Update(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<Post>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDelete()
    {
        // Arrange
        var id = 1;

        // Act
        await _service.DeleteAsync(id);

        // Assert
        _repository.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task IsUserPostAuthorAsync_UserIsAuthor_ShouldReturnTrue()
    {
        // Arrange
        var id = 1;
        var idUser = "test";
        var post = GetTestPost();

        _repository.Setup(x => x.GetByIdNoTrackingAsync(It.IsAny<int>())).ReturnsAsync(post);
        
        // Act
        var result = await _service.IsUserPostAuthorAsync(id, idUser);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public async Task IsUserPostAuthorAsync_UserIsNotAuthor_ShouldReturnFalse()
    {
        // Arrange
        var id = 1;
        var idUser = "idUser";
        var post = GetTestPost();

        _repository.Setup(x => x.GetByIdNoTrackingAsync(It.IsAny<int>())).ReturnsAsync(post);

        // Act
        var result = await _service.IsUserPostAuthorAsync(id, idUser);

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public async Task IsUserPostAuthorAsync_PostDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var id = 1;
        var idUser = "idUser";
        Post post = null;

        _repository.Setup(x => x.GetByIdNoTrackingAsync(It.IsAny<int>())).ReturnsAsync(post);

        // Act
        var result = await _service.IsUserPostAuthorAsync(id, idUser);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task CommentAsync_ShouldAddComment()
    {
        // Arrange
        var id = 1;
        var idUser = "idUser";
        var comment = "comment";
        var mock = new Mock<Post>();

        _repository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(mock.Object);

        // Act
        var result = await _service.CommentAsync(id, idUser, comment);

        // Assert
        mock.Verify(x => x.AddComment(It.IsAny<PostComment>()), Times.Once);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<Post>()), Times.Once);
    }
    
    [Fact]
    public async Task CommentAsync_PostDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var id = 1;
        var idUser = "idUser";
        var comment = "comment";
        var mock = new Mock<Post>();

        _repository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Post)null);

        // Act
        var result = await _service.CommentAsync(id, idUser, comment);

        // Assert
        Assert.False(result);
        mock.Verify(x => x.AddComment(It.IsAny<PostComment>()), Times.Never);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<Post>()), Times.Never);
    }
    
    [Fact]
    public async Task RemoveCommentAsync_ShouldRemoveComment()
    {
        // Arrange
        var id = 1;
        var idComment = 1;
        var mock = new Mock<Post>();

        _repository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(mock.Object);

        // Act
        var result = await _service.RemoveCommentAsync(id, idComment);

        // Assert
        mock.Verify(x => x.RemoveComment(It.IsAny<int>()), Times.Once);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<Post>()), Times.Once);
    }
    
    [Fact]
    public async Task RemoveCommentAsync_PostDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var id = 1;
        var idComment = 1;
        var mock = new Mock<Post>();

        _repository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Post)null);

        // Act
        var result = await _service.RemoveCommentAsync(id, idComment);

        // Assert
        Assert.False(result);
        mock.Verify(x => x.RemoveComment(It.IsAny<int>()), Times.Never);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<Post>()), Times.Never);
    }

    [Fact]
    public async Task IsUserPostCommentAuthorAsync_UserIsAuthor_ShouldReturnTrue()
    {
        // Arrange
        var id = 1;
        var idPostComment = 1;
        var idUser = "test";
        var comment = "comment";
        var mock = new Mock<Post>();
        var postComment = new PostComment(id, idUser, comment);

        _repository.Setup(x => x.GetByIdNoTrackingAsync(It.IsAny<int>())).ReturnsAsync(mock.Object);
        mock.Setup(x => x.GetPostCommentById(It.IsAny<int>())).Returns(postComment);

        // Act
        var result = await _service.IsUserPostCommentAuthorAsync(id, idPostComment, idUser);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public async Task IsUserPostCommentAuthorAsync_UserIsNotAuthor_ShouldReturnFalse()
    {
        // Arrange
        var id = 1;
        var idPostComment = 1;
        var idUser = "idUser";
        var comment = "comment";
        var post = GetTestPost();
        var mock = new Mock<Post>();
        var postComment = new PostComment(id, idUser, comment);

        _repository.Setup(x => x.GetByIdNoTrackingAsync(It.IsAny<int>())).ReturnsAsync(post);
        mock.Setup(x => x.GetPostCommentById(It.IsAny<int>())).Returns(postComment);

        // Act
        var result = await _service.IsUserPostCommentAuthorAsync(id, idPostComment, idUser);

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public async Task IsUserPostCommentAuthorAsync_PostDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var id = 1;
        var idPostComment = 1;
        var idUser = "idUser";
        var comment = "comment";
        var mock = new Mock<Post>();

        _repository.Setup(x => x.GetByIdNoTrackingAsync(It.IsAny<int>())).ReturnsAsync((Post)null);

        // Act
        var result = await _service.IsUserPostCommentAuthorAsync(id, idPostComment, idUser);

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public async Task IsUserPostCommentAuthorAsync_CommentDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var id = 1;
        var idPostComment = 1;
        var idUser = "idUser";
        var comment = "comment";
        var mock = new Mock<Post>();
        PostComment postComment = null;

        _repository.Setup(x => x.GetByIdNoTrackingAsync(It.IsAny<int>())).ReturnsAsync(mock.Object);
        mock.Setup(x => x.GetPostCommentById(It.IsAny<int>())).Returns(postComment);

        // Act
        var result = await _service.IsUserPostCommentAuthorAsync(id, idPostComment, idUser);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task LikeAsync_ShouldAddLike()
    {
        // Arrange
        var id = 1;
        var idUser = "idUser";
        var mock = new Mock<Post>();

        _repository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(mock.Object);

        // Act
        var result = await _service.LikeAsync(id, idUser);

        // Assert
        mock.Verify(x => x.Like(It.IsAny<PostLike>()), Times.Once);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<Post>()), Times.Once);
    }
    
    [Fact]
    public async Task LikeAsync_PostDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var id = 1;
        var idUser = "idUser";
        var mock = new Mock<Post>();

        _repository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Post)null);

        // Act
        var result = await _service.LikeAsync(id, idUser);

        // Assert
        Assert.False(result);
        mock.Verify(x => x.Like(It.IsAny<PostLike>()), Times.Never);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<Post>()), Times.Never);
    }
    
    [Fact]
    public async Task RemoveLikeAsync_ShouldRemoveLike()
    {
        // Arrange
        var id = 1;
        var idUser = "idUser";
        var mock = new Mock<Post>();

        _repository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(mock.Object);

        // Act
        var result = await _service.RemoveLikeAsync(id, idUser);

        // Assert
        mock.Verify(x => x.RemoveLike(It.IsAny<string>()), Times.Once);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<Post>()), Times.Once);
    }
    
    [Fact]
    public async Task RemoveLikeAsync_PostDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var id = 1;
        var idUser = "idUser";
        var mock = new Mock<Post>();

        _repository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Post)null);

        // Act
        var result = await _service.RemoveLikeAsync(id, idUser);

        // Assert
        Assert.False(result);
        mock.Verify(x => x.RemoveLike(It.IsAny<string>()), Times.Never);
        _repository.Verify(x => x.UpdateAsync(It.IsAny<Post>()), Times.Never);
    }
}