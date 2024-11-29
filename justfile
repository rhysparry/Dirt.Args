set windows-shell := ["pwsh.exe", "-NoProfile", "-c"]

export NuGetApiKey := env("NUGET_API_KEY", "")
export Version := trim_start_match(`git-cliff --bumped-version`, "v")

build-cmd := if os_family() == "windows" {
    "./build.cmd"
} else {
    "./build.sh"
}
mkdir-p := if os_family() == "windows" { "New-Item -Type Directory -Force" } else { "mkdir -p" }

restore:
    {{build-cmd}} -target Restore

build: 
    {{build-cmd}} -target Compile

test:
    {{build-cmd}} -target Test

pack:
    {{build-cmd}} -target Pack

clean:
    {{build-cmd}} -target Clean

nuget-push:
    {{build-cmd}} -target Publish
    
nuget-push-no-pack:
    {{build-cmd}} -target Publish -skip Pack

[private]
artifacts-dir:
    {{ mkdir-p }} artifacts

release-notes: artifacts-dir
    git-cliff --latest --strip header --output artifacts/RELEASE-NOTES.md

changelog:
    git-cliff --output CHANGELOG.md
