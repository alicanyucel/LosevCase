namespace Losev.Application.Dtos.User;

public record UserDto(
Guid Id,
string FirstName,
string LastName,
string FullName,
string IpAddress,
bool StatusSuccess,
DateTime DateTime,
bool IsDeleted
);
