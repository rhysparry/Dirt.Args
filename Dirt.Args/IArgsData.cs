namespace Dirt;

public interface IArgsData
{
    IReadOnlyDictionary<string, IFlagData> Flags { get; }
    IReadOnlyDictionary<string, string> ValueFlags { get; }
    IReadOnlyDictionary<string, IReadOnlyList<string>> MultiValueFlags { get; }
    IReadOnlyList<string> Remaining { get; }
}