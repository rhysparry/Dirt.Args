set windows-shell := ["pwsh.exe", "-NoProfile", "-c"]

export NuGetApiKey := env("NUGET_API_KEY", "")
export Version := trim_start_match(`git-cliff --bumped-version`, "v")

restore:
    ./build.cmd -target Restore

build: 
    ./build.cmd -target Compile

test:
    ./build.cmd -target Test

pack:
    ./build.cmd -target Pack

clean:
    ./build.cmd -target Clean

nuget-push:
    ./build.cmd -target Publish
    
nuget-push-no-pack:
    ./build.cmd -target Publish -skip Pack

changelog:
    git-cliff --output CHANGELOG.md
