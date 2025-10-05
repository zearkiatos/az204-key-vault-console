function build() {
    dotnet build KeyVaultConsole.csproj
}

function run() {
    dotnet run --project KeyVaultConsole.csproj
}