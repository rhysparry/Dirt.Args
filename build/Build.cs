using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;

class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Compile);
    
    AbsolutePath Artifacts => RootDirectory / "artifacts";

    [Solution(GenerateProjects = true)]
    readonly Solution Solution = null!;

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter("Version to use")] readonly string Version = "0.0.0-undefined";
    
    [Secret, Parameter("NuGet API key")] readonly string? NuGetApiKey;

    [UsedImplicitly]
    Target Clean => clean => clean
        .Before(Restore)
        .Executes(() =>
        {
            DotNetTasks.DotNetClean(s => s
                .SetProject(Solution)
                .SetConfiguration(Configuration)
            );
            Artifacts.CreateOrCleanDirectory();
        });

    Target Restore => restore => restore
        .Executes(() =>
        {
            DotNetTasks.DotNetRestore(s => s
                .SetProjectFile(Solution)
            );
        });

    Target Compile => compile => compile
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetTasks.DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetVersion(Version)
                .SetAssemblyVersion(Version)
                .EnableNoRestore()
            );
        });

    Target Test => test => test
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTasks.DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetVerbosity(DotNetVerbosity.normal)
                .EnableNoBuild()
            );
        });
    
    Target Pack => pack => pack
        .WhenSkipped(DependencyBehavior.Skip)
        .DependsOn(Test)
        .Executes(() =>
        {
            DotNetTasks.DotNetPack(s => s
                .SetProject(Solution.Dirt_Args)
                .SetConfiguration(Configuration)
                .SetVersion(Version)
                .SetOutputDirectory(Artifacts)
                .EnableNoBuild()
            );
        });
    
    [UsedImplicitly]
    Target Publish => publish => publish
        .DependsOn(Pack)
        .Requires(() => NuGetApiKey)
        .Executes(() =>
        {
            DotNetTasks.DotNetNuGetPush(s => s
                .SetSource("https://api.nuget.org/v3/index.json")
                .SetTargetPath(Artifacts / "*.nupkg")
                .SetApiKey(NuGetApiKey)
            );
        });
}
