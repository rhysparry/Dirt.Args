namespace Dirt;

public interface IFlagData
{
    public string Flag { get; }
    public int Count { get; }
    public IReadOnlyList<string> Values { get; }
}