# Dirt.Args

Dirt.Args is a simple library for parsing command line arguments in .NET.

Its goal is to require no configuration and be a step up from using the built-in `string[] args` parameter in your `Main` method.

For more advanced scenarios, you should consider using a more feature-rich library.

Specifically, the following features are *not* supported:

- Displaying usage
- Validation of parameters
- Commands
- Parsing to anything other than strings

The library does support the following:

- Boolean Flags (e.g. `--foo`)
- Key-Value Pairs (e.g. `--foo=bar`)
- Using `--` to separate flags from arguments (e.g. `--foo -- --bar`)
- Counting repeated instances of a flag (e.g. `--foo --foo`)
- Short flags (e.g. `-f`)
- Multiple values for a single flag (e.g. `--foo=bar --foo=baz`)

## Loose Arguments

By default, values must be assigned using `--flag=value`. The `LooseArgsSource` provides an alternative that supports `--flag value` syntax:

```csharp
var args = new Dirt.Args(new Dirt.LooseArgsSource());

// With: --output file.txt
var output = args.GetFlagValue("output"); // "file.txt"
```

Loose values are collected until the next flag or `--` separator:

```csharp
// With: --files a.txt b.txt --verbose
var files = args.GetMultiFlagValue("files"); // ["a.txt", "b.txt"]
var verbose = args.HasFlag("verbose");       // true
```

**Behavior notes:**
- Long flags (`--flag`) start a collection context for subsequent non-flag arguments
- Explicit values (`--flag=value`) do not start a collection context
- Short flags (`-f`) do not participate in loose argument collection
- The `--` separator stops all collection
- Standard `--flag=value` syntax continues to work alongside loose arguments

You can also wrap a custom `IArgsSource`:

```csharp
var args = new Dirt.Args(new Dirt.LooseArgsSource(myCustomSource));
```

## Usage

```csharp
var args = new Dirt.Args();

if (args.HasFlag("foo")) {
    Console.WriteLine("Foo is set!");
}
```

The `IArgs` interface exposes the following methods and properties

- `bool HasFlag(string flag)` - Returns true if the flag was set
- `int GetFlagCount(string flag)` - Gets the number of times the flag was set
- `string GetValueFlag(string flag)` - Gets the value of the flag, or null if it was not set
- `IReadOnlyList<string> GetMultiValueFlag(string flag)` - Gets the values of the flag, or an empty list if it was not set
- `IReadOnlyList<string> Remaining` - Any other arguments that are not flags


The `Args` class explicitly implements these properties from the `IArgsData` interface to allow access to the underlying data directly:

- `IReadOnlyDictionary<string, IFlagData> Flags` - A dictionary of all flags that were set and their values
- `IReadOnlyDictionary<string, string> ValueFlags` - A dictionary of all key-value pairs that were set
- `IReadOnlyDictionary<string, IReadOnlyList<string>> MultiValueFlags` - A dictionary of all value flags with all their values

The `ValueFlags` and `MultiValueFlags` properties are projections of the data exposed by the `Flags` property.

The `IFlagData` interface exposes the following properties:

- `string Flag` - The name of the flag
- `int Count` - The number of times the flag was set
- `IReadOnlyList<string> Values` - The values associated with the flag. This could be empty.

## License

MIT
