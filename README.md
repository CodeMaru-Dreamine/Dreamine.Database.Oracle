# Dreamine.Database.Oracle

[![CI](https://github.com/CodeMaru-Dreamine/Dreamine.Database.Oracle/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/CodeMaru-Dreamine/Dreamine.Database.Oracle/actions/workflows/ci.yml) [![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=CodeMaru-Dreamine_Dreamine.Database.Oracle&metric=alert_status&branch=main)](https://sonarcloud.io/summary/new_code?id=CodeMaru-Dreamine_Dreamine.Database.Oracle&branch=main) [![Security](https://sonarcloud.io/api/project_badges/measure?project=CodeMaru-Dreamine_Dreamine.Database.Oracle&metric=security_rating&branch=main)](https://sonarcloud.io/summary/new_code?id=CodeMaru-Dreamine_Dreamine.Database.Oracle&branch=main) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=CodeMaru-Dreamine_Dreamine.Database.Oracle&metric=coverage&branch=main)](https://sonarcloud.io/summary/new_code?id=CodeMaru-Dreamine_Dreamine.Database.Oracle&branch=main)

[![License](https://img.shields.io/github/license/CodeMaru-Dreamine/Dreamine.Database.Oracle?label=license)](./LICENSE) [![.NET](https://img.shields.io/badge/.NET-8-512BD4)](https://dotnet.microsoft.com/) [![NuGet](https://img.shields.io/nuget/v/Dreamine.Database.Oracle?label=nuget)](https://www.nuget.org/packages/Dreamine.Database.Oracle) [![Downloads](https://img.shields.io/nuget/dt/Dreamine.Database.Oracle?label=downloads)](https://www.nuget.org/packages/Dreamine.Database.Oracle)

[![Docs](https://img.shields.io/badge/📘_Docs-dreamine.kr-2F80ED)](https://dreamine.kr) [![Guide](https://img.shields.io/badge/📘_Guide-dreamine.kr-3498DB)](https://dreamine.kr) [![Playground](https://img.shields.io/badge/🎮_Playground-dreamine.kr-8E44AD)](https://dreamine.kr) [![Book](https://img.shields.io/badge/📖_Book-Practical_MVVM_Architecture-111111)](https://dreamine.kr)

`Dreamine.Database.Oracle` is the Oracle provider for the Dreamine Database package family.

[한국어 문서](./README_KO.md)

## Package Role

This package implements `IDatabaseProvider` for Oracle using `Oracle.ManagedDataAccess.Core`.

```text
Dreamine.Database.Abstractions
        ↑
Dreamine.Database.Core
        ↑
Dreamine.Database.Oracle
```

## Features

- Oracle connection creation
- Oracle parameter prefix support with `:`
- Uppercase quoted identifiers
- Oracle type mapping
- Identity column table creation
- CRUD support through the shared `DatabaseProviderBase`

## Quick Start

```csharp
using Dreamine.Database.Oracle;

var provider = new OracleDatabaseProvider(
    "User Id=dreamine;Password=password;Data Source=localhost:1521/XEPDB1;");

provider.EnsureDatabaseExists();
provider.CreateTable<SampleCustomer>();
provider.Insert(new SampleCustomer
{
    Name = "Dreamine",
    Role = "Operator",
    CreatedAt = DateTime.Now
});
```

## Schema Note

Oracle usually works with an existing user/schema instead of creating a database from the application connection string. Prepare the user/schema first and grant table creation permissions to that account.

## Dependencies

- `Dreamine.Database.Abstractions`
- `Dreamine.Database.Core`
- `Oracle.ManagedDataAccess.Core`

## Target Framework

```text
net8.0
```

## Samples and Tests

- Unit tests: `20_SOURCES/200. Tests/Dreamine.FullKit.Tests/Database`
- WPF sample: `20_SOURCES/998. DEMO/000. Sample/010. Wpfs/SampleSmart/Pages/PageSub/PageDatabase.xaml`

## License

MIT License
