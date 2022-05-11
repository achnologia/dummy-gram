using System.Linq;
using System.Threading.Tasks;
using DummyGram.Domain.Entities;
using Xunit;

namespace DummyGram.Domain.UnitTests.Entities;

public class AppUser_Tests
{
    [Fact]
    public async Task UpdateDisplayName_ShouldUpdate()
    {
        // Arrange
        var email = "test@email.com";
        var newDisplayName = "New display name";
        var user = new AppUser(email);

        // Act
        user.Update(newDisplayName);

        // Assert
        Assert.Equal(newDisplayName, user.DisplayName);
    }

    [Fact]
    public async Task Subscribe_ShouldSubscribe()
    {
        // Arrange
        var email = "test@email.com";
        var subscriberEmail = "test1@email.com";
        var user = new AppUser(email);
        var userSubscriber = new AppUser(subscriberEmail);

        // Act
        user.Subscribe(userSubscriber);

        // Assert
        Assert.Equal(1, user.Subscribers.Count());
        Assert.Equal(userSubscriber, user.Subscribers.First());
    }
    
    [Fact]
    public async Task Unsubscribe_ShouldUnsubscribe()
    {
        // Arrange
        var email = "test@email.com";
        var subscriberEmail = "test1@email.com";
        var user = new AppUser(email);
        var userSubscriber = new AppUser(subscriberEmail);

        // Act
        user.Subscribe(userSubscriber);
        user.Unsubscribe(userSubscriber);

        // Assert
        Assert.Equal(0, user.Subscribers.Count());
    }

    [Fact]
    public async Task HasSubscriber_ShouldReturnTrue()
    {
        // Arrange
        var email = "test@email.com";
        var subscriberEmail = "test1@email.com";
        var user = new AppUser(email);
        var userSubscriber = new AppUser(subscriberEmail);

        // Act
        user.Subscribe(userSubscriber);
        var result = user.HasSubscriber(userSubscriber);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public async Task DoesNotHaveSubscriber_ShouldReturnFalse()
    {
        // Arrange
        var email = "test@email.com";
        var subscriberEmail = "test1@email.com";
        var user = new AppUser(email);
        var userSubscriber = new AppUser(subscriberEmail);

        // Act
        var result = user.HasSubscriber(userSubscriber);

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public async Task SavePost_ShouldSavePost()
    {
        // Arrange
        var test = "test";
        var email = "test@email.com";
        var user = new AppUser(email);
        var post = new Post(test, test, test);

        // Act
        user.SavePost(post);

        // Assert
        Assert.Equal(1, user.SavedPosts.Count());
        Assert.Equal(post, user.SavedPosts.First());
    }
    
    [Fact]
    public async Task RemoveSavedPost_ShouldRemoveSavedPost()
    {
        // Arrange
        var test = "test";
        var email = "test@email.com";
        var user = new AppUser(email);
        var post = new Post(test, test, test);

        // Act
        user.SavePost(post);
        user.RemoveSavedPost(post);

        // Assert
        Assert.Equal(0, user.SavedPosts.Count());
    }
}