using Ardalis.SmartEnum;

namespace Losev.Domain.Enums;

public sealed class GroupType : SmartEnum<GroupType>
{
    public static readonly GroupType WebGroup = new(nameof(WebGroup), 1);
    public static readonly GroupType MobileGroup = new(nameof(MobileGroup), 2);
    public static readonly GroupType DesktopGroup = new(nameof(DesktopGroup), 3);
    private GroupType(string name, int value) : base(name, value)
    {
    }
}
