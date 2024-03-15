namespace Dirt;

public interface IArgs
{
    bool HasFlag(string flag);
    int GetFlagCount(string flag);
    string? GetFlagValue(string flag);
    IReadOnlyList<string> GetMultiFlagValue(string flag);
    IReadOnlyList<string> Remaining { get; }
}