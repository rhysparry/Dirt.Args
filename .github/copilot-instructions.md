# GitHub Copilot Instructions for Dirt.Args

## Project Overview

Dirt.Args is a simple, no-configuration command-line argument parsing library for .NET. The goal is to provide a lightweight alternative to the built-in `string[] args` parameter without requiring extensive configuration.

**Key Philosophy:**
- Minimal configuration required
- Simple API focused on common use cases
- Does NOT support: usage display, validation, commands, loose arguments, or non-string parsing
- DOES support: boolean flags, key-value pairs, `--` separator, flag counting, short flags, and multi-value flags

## Project Structure

- `Dirt.Args/` - Main library source code
- `Dirt.Args.Tests/` - xUnit test project
- `build/` - Build configuration files (Nuke build)
- `.github/workflows/` - CI/CD workflows
- `justfile` - Task runner commands (use `just` for common tasks)

## Build and Test Process

### Prerequisites
- .NET 10.0 SDK
- `just` command runner
- `git-cliff` for changelog generation
- Pre-commit hooks (installed via `pre-commit install`)

### Common Commands
```bash
# Restore dependencies
just restore

# Build the project
just build

# Run tests
just test

# Run linting/formatting
just lint

# Create NuGet package
just pack
```

### CI/CD
- Automated tests run on Ubuntu and Windows via GitHub Actions
- Tests are run using `just test` which calls the Nuke build system
- Pre-commit hooks (when installed) enforce code formatting and commit message standards locally

## Coding Conventions

### C# Style
- Use **C# 13** features (primary constructors, file-scoped namespaces, etc.)
- Follow standard .NET naming conventions (PascalCase for public members, camelCase for parameters)
- Use nullable reference types consistently
- Format code using **CSharpier** (automatically applied via pre-commit hooks)
- Add XML documentation comments for all public APIs

### Code Organization
- Namespace: `Dirt` for main library, `Dirt.Tests` for tests
- Prefer expression-bodied members where appropriate
- Use modern C# patterns (pattern matching, collection expressions when applicable)
- Keep classes focused and single-purpose

### Testing
- Use xUnit for all tests
- Test class names should end with `Test` (e.g., `ArgsTest`)
- Use descriptive test method names that explain the scenario
- Use `Assert` methods from xUnit (not FluentAssertions or other libraries)
- Suppress CA1861 (constant arrays) in test files where needed

## Commit Message Conventions

This project uses **conventional commits** enforced by commitlint:

Format: `<type>: <subject>`

**Allowed types:**
- `feat` - New feature
- `fix` - Bug fix
- `docs` - Documentation changes
- `style` - Code style/formatting (no logic changes)
- `refactor` - Code refactoring
- `perf` - Performance improvements
- `test` - Test additions/changes
- `build` - Build system changes
- `ci` - CI/CD changes
- `chore` - Other changes (dependencies, tooling)
- `revert` - Revert a previous commit

**Rules:**
- Header: 10-50 characters
- Body lines: max 72 characters
- Footer lines: max 72 characters

**Examples:**
```
feat: add support for multi-value flags
fix: handle empty flag values correctly
docs: update README with usage examples
test: add tests for short flag parsing
```

## Linting and Code Quality

### Pre-commit Hooks
The project uses pre-commit hooks that run automatically:
- **CSharpier** - C# code formatting
- **actionlint** - GitHub Actions workflow validation
- **commitlint** - Commit message validation
- Standard checks: JSON/YAML validation, trailing whitespace, EOF fixes

Run manually with: `just lint`

### Code Analysis
- Enable nullable reference types
- Address compiler warnings
- Follow .NET code analysis rules

## Dependencies

### When Adding Dependencies
- Prefer standard .NET libraries over third-party packages
- Keep dependencies minimal (aligns with project philosophy)
- Update NuGet packages via Dependabot (configured to run weekly)
- Test dependencies are grouped together in Dependabot config

### Current Key Dependencies
- **xUnit v3** - Testing framework
- **Microsoft.NET.Test.Sdk** - Test SDK
- **Nuke** - Build automation

## API Design Guidelines

When extending the library:
- Maintain backwards compatibility
- Follow the existing pattern: `IArgs` interface with `Args` implementation
- Use read-only collections for return values
- Return null or empty collections (not exceptions) for missing values
- Keep the API surface small and focused

## Common Scenarios

### Adding a New Flag Type
1. Update `IArgs` interface with new method
2. Implement in `Args` class
3. Add corresponding tests in `ArgsTest`
4. Update README if it's a public-facing feature

### Fixing a Bug
1. Write a failing test that reproduces the issue
2. Fix the implementation
3. Verify the test passes and no existing tests break
4. Update changelog if needed (handled via git-cliff)

### Making Documentation Changes
1. Update README.md for public-facing changes
2. Update XML doc comments for API changes
3. No need to run full test suite for docs-only changes

## Release Process

Releases are managed using git-cliff and semantic versioning:
- Run `just release` from main branch (requires no pending changes)
- Version is auto-bumped based on conventional commits
- Changelog is auto-generated
- Tags are created automatically

**Do not manually:**
- Create version tags
- Edit CHANGELOG.md directly
- Bump version numbers in project files

## GitHub Workflows

- **dotnet-ci-build.yml** - Runs tests on push/PR to main
- **pre-commit.yml** - Validates pre-commit config
- **nuget-publish.yml** - Publishes packages to NuGet

## Questions or Clarifications

If unsure about any convention or practice:
1. Look for similar examples in the existing codebase
2. Follow .NET best practices and conventions
3. Keep changes minimal and focused
4. Maintain the project's philosophy of simplicity
