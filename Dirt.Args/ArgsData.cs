namespace Dirt;

internal class ArgsData : IArgsData
{
    private readonly Lazy<IReadOnlyDictionary<string, string>> _lazyValueFlags;

    private readonly Lazy<IReadOnlyDictionary<string, IReadOnlyList<string>>> _lazyMultiValueFlags;

    public ArgsData(IArgsSource args)
        : this(args.GetArgs()) { }

    public ArgsData(IEnumerable<string> args)
    {
        var flags = new Dictionary<string, FlagData>();
        var remaining = new List<string>();

        using var argsEnumerator = args.GetEnumerator();
        while (argsEnumerator.MoveNext())
        {
            var arg = argsEnumerator.Current;
#if NETSTANDARD2_0
            if (arg == null)
                break;
#endif
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
#if NETSTANDARD2_0
                var flagValue = arg.Substring(2);
#else
                var flagValue = arg[2..];
#endif
                var flagValueSplit = flagValue.Split('=', 2);
                if (flagValueSplit.Length == 2)
                {
                    var flag = flagValueSplit[0];
                    var value = flagValueSplit[1];
                    flags.AddFlagValue(flag, value);
                }
                else
                {
                    flags.IncrementFlag(flagValue);
                }
            }
            else if (arg.StartsWith('-'))
            {
#if NETSTANDARD2_0
                var flagValue = arg.Substring(1);
#else
                var flagValue = arg[1..];
#endif
                foreach (var flag in flagValue)
                {
                    flags.IncrementFlag(flag.ToString());
                }
            }
            else
            {
                remaining.Add(arg);
            }
        }

        Flags = flags.AsReadOnly();
        Remaining = remaining;

        _lazyValueFlags = new Lazy<IReadOnlyDictionary<string, string>>(
            () =>
                Flags
                    .Values.Where(f => f.Values.Count > 0)
                    .ToDictionary(f => f.Flag, f => f.Values[0])
        );

        _lazyMultiValueFlags = new Lazy<IReadOnlyDictionary<string, IReadOnlyList<string>>>(
            () =>
                Flags.Values.Where(f => f.Values.Count > 0).ToDictionary(f => f.Flag, f => f.Values)
        );
    }

    public IReadOnlyDictionary<string, IFlagData> Flags { get; }
    public IReadOnlyDictionary<string, string> ValueFlags => _lazyValueFlags.Value;
    public IReadOnlyDictionary<string, IReadOnlyList<string>> MultiValueFlags =>
        _lazyMultiValueFlags.Value;
    public IReadOnlyList<string> Remaining { get; }
}
