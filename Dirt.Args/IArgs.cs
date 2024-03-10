namespace Dirt;

public interface IArgs
{
    IReadOnlyList<string> Flags { get; }
    IReadOnlyDictionary<string, string> ValueFlags { get; }
    IReadOnlyDictionary<string, IReadOnlyList<string>> MultiValueFlags { get; }
    IReadOnlyList<string> Remaining { get; }

    bool HasFlag(string flag);
    int GetFlagCount(string flag);
    string? GetFlagValue(string flag);
    IReadOnlyList<string> GetMultiFlagValue(string flag);
}