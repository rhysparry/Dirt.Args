namespace Dirt;

internal class FlagData(string flag) : IFlagData
{
    public FlagData(string flag, string value) : this(flag) => Values = new[] { value };
    public string Flag { get; } = flag;
#if NETSTANDARD2_0
    public int Count { get; private set; } = 1;
    public IReadOnlyList<string> Values { get; private set; } = [];
#else
    public int Count { get; private init; } = 1;
    public IReadOnlyList<string> Values { get; private init; } = [];
#endif

    public FlagData AddValue(string value) => new(Flag)
    {
        Count = Count + 1,
        Values = Values.Append(value).ToList()
    };

    public FlagData Increment() => new(Flag)
    {
        Count = Count + 1,
        Values = Values
    };
}