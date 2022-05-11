using System.Threading.Tasks;
using System.Linq;
using DummyGram.Domain.Entities;
using Xunit;

namespace DummyGram.Domain.UnitTests.Entities;

public class Post_Tests
{
    [Fact]
    public async Task Update_ShouldUpdate()
    {
        // Arrange
        var test = "test";
        var newValue = "new value";
        var post = new Post(test, test, test);

        // Act
        post.Update(newValue, newValue);

        // Assert
        Assert.Equal(newValue, post.ImageUrl);
        Assert.Equal(newValue, post.Description);
    }

    [Fact]
    public async Task AddComment_ShouldAddComment()
    {
        // Arrange
        var test = "test";
        var post = new Post(test, test, test);
        var postComment = new PostComment(post.Id, "idUser", test);

        // Act
        post.AddComment(postComment);

        // Assert
        Assert.Equal(1, post.Comments.Count());
        Assert.Equal(postComment, post.Comments.First());
    }
    
    [Fact]
    public async Task GetPostCommentById_ShouldReturnComment()
    {
        // Arrange
        var test = "test";
        var post = new Post(test, test, test);
        var postComment = new PostComment(post.Id, "idUser", test);

        // Act
        post.AddComment(postComment);
        var result = post.GetPostCommentById(0);

        // Assert
        Assert.Equal(postComment, result);
    }
    
    [Fact]
    public async Task RemoveComment_ShouldRemoveComment()
    {
        // Arrange
        var test = "test";
        var post = new Post(test, test, test);
        var postComment = new PostComment(post.Id, "idUser", test);

        // Act
        post.AddComment(postComment);
        post.RemoveComment(0);

        // Assert
        Assert.Equal(0, post.Comments.Count());
    }
    
    [Fact]
    public async Task Like_ShouldAddLike()
    {
        // Arrange
        var test = "test";
        var post = new Post(test, test, test);
        var postLike = new PostLike(post.Id, "idUser");

        // Act
        post.Like(postLike);

        // Assert
        Assert.Equal(1, post.Likes.Count());
        Assert.Equal(postLike, post.Likes.First());
    }

    [Fact]
    public async Task RemoveLike_ShouldRemoveLike()
    {
        // Arrange
        var test = "test";
        var idUser = "idUser";
        var post = new Post(test, test, test);
        var postLike = new PostLike(post.Id, idUser);

        // Act
        post.Like(postLike);
        post.RemoveLike(idUser);

        // Assert
        Assert.Equal(0, post.Likes.Count());
    }
}