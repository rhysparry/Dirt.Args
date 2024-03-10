namespace Dirt;

public class Args : IArgs
{
    private readonly Dictionary<string, int> _flags;

    public Args() : this (Environment.GetCommandLineArgs())
    {
    }

    public Args(IEnumerable<string> args)
    {
        var flags = new Dictionary<string, int>();
        var valueFlags = new Dictionary<string, string>();
        var multiValueFlags = new Dictionary<string, List<string>>();
        var remaining = new List<string>();

        using var argsEnumerator = args.GetEnumerator();
        while (argsEnumerator.MoveNext())
        {
            var arg = argsEnumerator.Current;
            if (arg == "--")
            {
                while (argsEnumerator.MoveNext())
                {
                    remaining.Add(argsEnumerator.Current);
                }
                break;
            }
            if (arg.StartsWith("--"))
            {
                var flagValue = arg[2..];
                var flagValueSplit = flagValue.Split('=', 2);
                if (flagValueSplit.Length == 2)
                {
                    var flag = flagValueSplit[0];
                    var value = flagValueSplit[1];
                    valueFlags.TryAdd(flag, value);
                    var multiFlagList = multiValueFlags.GetValueOrDefault(flag, []);
                    multiFlagList.Add(value);
                    multiValueFlags[flag] = multiFlagList;
                    flags[flag] = flags.GetValueOrDefault(flag, 0) + 1;
                }
                else
                {
                    flags[flagValue] = flags.GetValueOrDefault(flagValue, 0) + 1;
                }
            }
            else if (arg.StartsWith('-'))
            {
                var flagValue = arg[1..];
                foreach (var flag in flagValue)
                {
                    flags[flag.ToString()] = flags.GetValueOrDefault(flag.ToString(), 0) + 1;
                }
            }
            else
            {
                remaining.Add(arg);
            }
        }

        _flags = flags;
        Flags = flags.Keys.ToList();
        ValueFlags = valueFlags;
        MultiValueFlags = multiValueFlags.ToDictionary(x => x.Key, x => x.Value as IReadOnlyList<string>);
        Remaining = remaining;
    }

    public IReadOnlyList<string> Flags { get; }
    public IReadOnlyDictionary<string, string> ValueFlags { get; }
    public IReadOnlyDictionary<string, IReadOnlyList<string>> MultiValueFlags { get; }
    public IReadOnlyList<string> Remaining { get; }

    public bool HasFlag(string flag) => _flags.ContainsKey(flag);
    public int GetFlagCount(string flag) => _flags.GetValueOrDefault(flag, 0);

    public string? GetFlagValue(string flag)
    {
        var values = GetMultiFlagValue(flag);
        return values.Count > 0 ? values[0] : null;
    }

    public IReadOnlyList<string> GetMultiFlagValue(string flag) => MultiValueFlags.GetValueOrDefault(flag, Array.Empty<string>());
}