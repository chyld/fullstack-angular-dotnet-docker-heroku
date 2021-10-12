# Sigma

```sh
dotnet new sln -o sigma

cd sigma
dotnet new tool-manifest
dotnet tool install dotnet-ef
dotnet tool install Microsoft.dotnet-httprepl

dotnet new classlib -o lib
dotnet new xunit -o test
dotnet new webapi --no-https -o web

dotnet sln add lib/lib.csproj
dotnet sln add test/test.csproj
dotnet sln add web/web.csproj

cd web
dotnet add reference ../lib/lib.csproj
dotnet add package Microsoft.Extensions.Logging.Console
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore

cd test
dotnet add reference ../lib/lib.csproj
dotnet add reference ../web/web.csproj
dotnet add package FluentAssertions
```
