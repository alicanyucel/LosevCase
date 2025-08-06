using Losev.Application.Features.Group.GetByIdGroup;
using Losev.Domain.Entities;
using Losev.Domain.Repositories;
using Moq;

public class GetGroupByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenGroupIsNull()
    {
        var mockRepo = new Mock<IGroupRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Group?)null);

        var handler = new GetGroupByIdQueryHandler(mockRepo.Object);

        var result = await handler.Handle(new GetGroupByIdQuery(Guid.NewGuid()), CancellationToken.None);

        Assert.False(result.IsSuccessful);
        Assert.Contains("Grup bulunamadı.", result.ErrorMessages);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenGroupIsDeleted()
    {
        var group = new Group
        {
            Id = Guid.NewGuid(),
            IsDeleted = true,
            Name = "Deleted Group",
            GroupType = default,
            PassName = "pass",
            Url = "url",
            AppUserId = Guid.NewGuid()
        };

        var mockRepo = new Mock<IGroupRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(group.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(group);

        var handler = new GetGroupByIdQueryHandler(mockRepo.Object);

        var result = await handler.Handle(new GetGroupByIdQuery(group.Id), CancellationToken.None);

        Assert.False(result.IsSuccessful);
        Assert.Contains("Grup bulunamadı.", result.ErrorMessages);
    }

    [Fact]
    public async Task Handle_ShouldReturnGroupDto_WhenGroupExistsAndIsNotDeleted()
    {
        var group = new Group
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
            Name = "Active Group",
            GroupType = default,
            PassName = "pass",
            Url = "url",
            AppUserId = Guid.NewGuid()
        };

        var mockRepo = new Mock<IGroupRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(group.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(group);

        var handler = new GetGroupByIdQueryHandler(mockRepo.Object);

        var result = await handler.Handle(new GetGroupByIdQuery(group.Id), CancellationToken.None);

        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.Data);
        Assert.Equal(group.Id, result.Data.Id);
        Assert.Equal(group.Name, result.Data.Name);
        Assert.Equal(group.GroupType, result.Data.GroupType);
        Assert.Equal(group.PassName, result.Data.PassName);
        Assert.Equal(group.Url, result.Data.Url);
        Assert.Equal(group.AppUserId, result.Data.AppUserId);
    }
}
