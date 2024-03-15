using System.Diagnostics.CodeAnalysis;

namespace Dirt.Tests;

[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class ArgsTest
{
    [Fact]
    public void WhenNoFlagsAllArgsInRemainder()
    {
        var rawArguments = new[] { "one", "two", "three" };
        var args = new Args(rawArguments);
        Assert.Equal(rawArguments, args.Remaining);
    }

    [Fact]
    public void FlagAtStartOfArgumentsIsCaptured()
    {
        var rawArguments = new[] { "--flag", "value" };
        var args = new Args(rawArguments);
        Assert.True(args.HasFlag("flag"));
        Assert.Equal(1, args.GetFlagCount("flag"));
        Assert.Equal(new[] { "value" }, args.Remaining);
    }

    [Fact]
    public void FlagWithValue()
    {
        var rawArguments = new[] { "--flag=value" };
        var args = new Args(rawArguments);
        Assert.True(args.HasFlag("flag"));
        Assert.Equal(1, args.GetFlagCount("flag"));
        Assert.Equal("value", args.GetFlagValue("flag"));
        Assert.Equal(new[] { "value" }, args.GetMultiFlagValue("flag"));
    }

    [Fact]
    public void ShortFlagUsedMultipleTimes()
    {
        // ReSharper disable once StringLiteralTypo
        var rawArguments = new[] { "-aabc" };
        var args = new Args(rawArguments);
        Assert.True(args.HasFlag("a"));
        Assert.Equal(2, args.GetFlagCount("a"));
        Assert.True(args.HasFlag("b"));
        Assert.Equal(1, args.GetFlagCount("b"));
        Assert.True(args.HasFlag("c"));
        Assert.Equal(1, args.GetFlagCount("c"));
    }

    [Fact]
    public void FlagWithMultipleValues()
    {
        var rawArguments = new[] { "--flag=value1", "--flag=value2" };
        var args = new Args(rawArguments);
        Assert.True(args.HasFlag("flag"));
        Assert.Equal(2, args.GetFlagCount("flag"));
        Assert.Equal("value1", args.GetFlagValue("flag"));
        Assert.Equal(new[] { "value1", "value2" }, args.GetMultiFlagValue("flag"));
    }

    [Fact]
    public void RemainingArgsAfterDoubleDash()
    {
        var rawArguments = new[] { "--flag", "--", "one", "two", "three", "--another" };
        var args = new Args(rawArguments);
        Assert.True(args.HasFlag("flag"));
        Assert.Equal(1, args.GetFlagCount("flag"));
        Assert.False(args.HasFlag("another"));
        Assert.Equal(new[] { "one", "two", "three", "--another" }, args.Remaining);
    }

    [Fact]
    public void FlagsInMiddleAreParsedEverythingElseGoesToRemaining()
    {
        var rawArguments = new[] { "one", "--flag", "two", "three", "--another", "four" };
        var args = new Args(rawArguments);
        Assert.True(args.HasFlag("flag"));
        Assert.Equal(1, args.GetFlagCount("flag"));
        Assert.True(args.HasFlag("another"));
        Assert.Equal(1, args.GetFlagCount("another"));
        Assert.Equal(new[] { "one", "two", "three", "four" }, args.Remaining);
    }

    [Fact]
    public void FlagValueDictionaryOnlyKeepsFirstValue()
    {
        var rawArguments = new[] { "--flag=value1", "--flag=value2" };
        var args = new Args(rawArguments);
        var argsData = args as IArgsData;
        Assert.Equal("value1", argsData.ValueFlags["flag"]);
        Assert.Equal(argsData.ValueFlags["flag"], argsData.MultiValueFlags["flag"][0]);
    }

    [Fact]
    public void FlagDefaultConstructorUsesCommandLineArgsDoesNotFail()
    {
        var args = new Args();
        Assert.NotNull(args);
    }

    [Fact]
    public void FlagsListDoesNotContainDuplicates()
    {
        var rawArguments = new []{ "--flag", "--flag" };
        var args = new Args(rawArguments);
        var argsData = args as IArgsData;
        Assert.Single(argsData.Flags);
        Assert.Equal("flag", argsData.Flags.Keys.First());
    }
}