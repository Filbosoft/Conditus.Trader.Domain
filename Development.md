# Technical docs

## Development environment

### Dependency installation
- [NuGet](https://docs.microsoft.com/en-us/nuget/reference/nuget-exe-cli-reference)

## How to create new version
1. Alter the code
2. Ensure it builds: `dotnet build`
3. Update the package version in the .csproj file
4. Package it: `dotnet pack`
5. Test it
    1. Add the new package to your local repo: `nuget add {Path\to\newly\created\package} -Source {Path\to\repo}`
    2. (If not already done) Add local nuget repo to your nuget config in test projectet: `dotnet nuget add source {Path\to\repo}`
    3. Add it to test project: `dotnet add [(Optional) {project}] package {PackageId} -v {NewVersion}`
    4. Test it