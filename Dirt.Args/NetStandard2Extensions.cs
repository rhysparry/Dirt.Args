using System.Runtime.CompilerServices;

#if NETSTANDARD2_0
namespace Dirt;

internal static class NetStandard2Extensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T? GetValueOrDefault<T>(
        this IReadOnlyDictionary<string, T> dictionary,
        string key
    ) => dictionary.TryGetValue(key, out var value) ? value : default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string[] Split(this string value, char separator, int count) =>
        value.Split([separator], count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool StartsWith(this string value, char prefix) =>
        value.Length > 0 && value[0] == prefix;
}
#endif
