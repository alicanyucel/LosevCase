using Losev.Application.Extensions;
using Losev.Application.Features.Group.GetAllGroup;
using Losev.Domain.Entities;
using Losev.Domain.Enums;
using Losev.Domain.Repositories;
using Moq;

public class GetAllGroupQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnOnlyActiveGroups()
    {
        var mockRepo = new Mock<IGroupRepository>();

        var groups = new List<Group>
        {
            new Group
            {
                Id = Guid.NewGuid(),
                Name = "Group 1",
                GroupType = GroupType.MobileGroup,
                PassName = "Pass1",
                Url = "http://example.com/1",
                Password = "123",
                AppUserId = Guid.NewGuid(),
                IsDeleted = false
            },
            new Group
            {
                Id = Guid.NewGuid(),
                Name = "Group 2",
                GroupType = GroupType.DesktopGroup,
                PassName = "Pass2",
                Url = "http://example.com/2",
                Password = "456",
                AppUserId = Guid.NewGuid(),
                IsDeleted = true
            }
        };

        mockRepo.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(groups);

        var handler = new GetAllGroupQueryHandler(mockRepo.Object);

        var result = await handler.Handle(new GetAllGroupsQuery(), CancellationToken.None);

        Assert.True(result.IsSuccessful);
        Assert.Single(result.Data);
        Assert.Equal("Group 1", result.Data.First().Name);
    }
}
