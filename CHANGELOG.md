# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.2.3] - 2024-12-07

### Added

- Add testing on net48 runtime ([#44](https://github.com/rhysparry/Dirt.Args/pull/44)) (Resolves [#39](https://github.com/rhysparry/Dirt.Args/issues/39))
- Add recipes to assist in releasing ([#51](https://github.com/rhysparry/Dirt.Args/pull/51))

### Changed

- Apply `EmbedUntrackedSources` ([#35](https://github.com/rhysparry/Dirt.Args/pull/35))
- Bump Nuke.Common from 8.1.1 to 8.1.2 ([#36](https://github.com/rhysparry/Dirt.Args/pull/36))
- Bump Nuke.Common from 8.1.2 to 8.1.4 ([#37](https://github.com/rhysparry/Dirt.Args/pull/37))
- Bump Microsoft.NET.Test.Sdk in the test-dependencies group ([#40](https://github.com/rhysparry/Dirt.Args/pull/40))
- Bump Nuke.Common from 8.1.4 to 9.0.1 ([#41](https://github.com/rhysparry/Dirt.Args/pull/41))
- Update build schema ([#42](https://github.com/rhysparry/Dirt.Args/pull/42))
- Link to pull requests in changelog ([#45](https://github.com/rhysparry/Dirt.Args/pull/45))
- Enable generation of release notes ([#46](https://github.com/rhysparry/Dirt.Args/pull/46))
- Configure dependabot to manage sdk updates ([#47](https://github.com/rhysparry/Dirt.Args/pull/47))
- Bump Nuke.Common from 9.0.1 to 9.0.3 ([#49](https://github.com/rhysparry/Dirt.Args/pull/49))
- Bump dotnet-sdk from 8.0.303 to 8.0.404 ([#48](https://github.com/rhysparry/Dirt.Args/pull/48))
- Generate release notes for GitHub ([#50](https://github.com/rhysparry/Dirt.Args/pull/50))

### Removed

- Remove net6.0 references ([#43](https://github.com/rhysparry/Dirt.Args/pull/43)) (Resolves [#38](https://github.com/rhysparry/Dirt.Args/issues/38))

## [1.2.2] - 2024-10-13

### Changed

- Update changelog in main
- Bump Nuke.Common from 8.1.0 to 8.1.1 ([#30](https://github.com/rhysparry/Dirt.Args/pull/30))
- Use `just` to manage build ([#31](https://github.com/rhysparry/Dirt.Args/pull/31))
- Enable deterministic builds ([#32](https://github.com/rhysparry/Dirt.Args/pull/32))
- Set .gitattributes for generated files ([#33](https://github.com/rhysparry/Dirt.Args/pull/33))
- Build.cmd invocation in justfile ([#34](https://github.com/rhysparry/Dirt.Args/pull/34))
- Update changelog for release 1.2.2

## [1.2.1] - 2024-10-06

### Fixed

- Set version in build parameters

## [1.2.0] - 2024-10-06

### Added

- Add XML documentation

### Changed

- Link issues in changelog
- Improve changelog grouping
- Conventional commits for dependabot
- Update dependabot configuration
- Bump xunit from 2.7.0 to 2.7.1 ([#8](https://github.com/rhysparry/Dirt.Args/pull/8))
- Bump xunit.runner.visualstudio from 2.5.7 to 2.5.8 ([#9](https://github.com/rhysparry/Dirt.Args/pull/9))
- Bump xunit from 2.7.1 to 2.8.0 ([#10](https://github.com/rhysparry/Dirt.Args/pull/10))
- Bump xunit.runner.visualstudio from 2.5.8 to 2.8.0 ([#11](https://github.com/rhysparry/Dirt.Args/pull/11))
- Group test dependency updates ([#15](https://github.com/rhysparry/Dirt.Args/pull/15))
- Bump the test-dependencies group with 3 updates ([#16](https://github.com/rhysparry/Dirt.Args/pull/16))
- Bump the test-dependencies group with 2 updates ([#17](https://github.com/rhysparry/Dirt.Args/pull/17))
- Switch to Nuke build ([#18](https://github.com/rhysparry/Dirt.Args/pull/18))
- Bump Microsoft.NET.Test.Sdk in the test-dependencies group ([#19](https://github.com/rhysparry/Dirt.Args/pull/19))
- Bump Microsoft.NET.Test.Sdk in the test-dependencies group ([#20](https://github.com/rhysparry/Dirt.Args/pull/20))
- Bump Nuke.Common from 8.0.0 to 8.1.0 ([#21](https://github.com/rhysparry/Dirt.Args/pull/21))
- Bump xunit in the test-dependencies group ([#22](https://github.com/rhysparry/Dirt.Args/pull/22))
- Bump xunit in the test-dependencies group ([#23](https://github.com/rhysparry/Dirt.Args/pull/23))
- Update Nuke schema ([#24](https://github.com/rhysparry/Dirt.Args/pull/24))
- Enable commitlint ([#25](https://github.com/rhysparry/Dirt.Args/pull/25))
- Include symbols in NuGet package ([#28](https://github.com/rhysparry/Dirt.Args/pull/28))
- Use `pwsh` as `justfile` shell on Windows ([#29](https://github.com/rhysparry/Dirt.Args/pull/29))
- Release v1.2.0

### Fixed

- Use hardcoded git-cliff repo information ([#26](https://github.com/rhysparry/Dirt.Args/pull/26))
- Set version without requiring `sed` ([#27](https://github.com/rhysparry/Dirt.Args/pull/27))

## [1.1.0] - 2024-03-23

### Added

- Add support for netstandard2.0 (Resolves [#3](https://github.com/rhysparry/Dirt.Args/issues/3))
- Add a changelog (Resolves [#6](https://github.com/rhysparry/Dirt.Args/issues/6))

### Changed

- Update CI Build to use justfile

### Fixed

- Fix changelog tag version format
- Dependabot commit message config

[1.2.3]: https://github.com/rhysparry/Dirt.Args/compare/v1.2.2..v1.2.3
[1.2.2]: https://github.com/rhysparry/Dirt.Args/compare/v1.2.1..v1.2.2
[1.2.1]: https://github.com/rhysparry/Dirt.Args/compare/v1.2.0..v1.2.1
[1.2.0]: https://github.com/rhysparry/Dirt.Args/compare/v1.1.0..v1.2.0
[1.1.0]: https://github.com/rhysparry/Dirt.Args/compare/v1.0.1..v1.1.0

<!-- generated by git-cliff -->
