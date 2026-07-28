# Dreamine.Database.Oracle

[![CI](https://github.com/CodeMaru-Dreamine/Dreamine.Database.Oracle/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/CodeMaru-Dreamine/Dreamine.Database.Oracle/actions/workflows/ci.yml) [![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=CodeMaru-Dreamine_Dreamine.Database.Oracle&metric=alert_status&branch=main)](https://sonarcloud.io/summary/new_code?id=CodeMaru-Dreamine_Dreamine.Database.Oracle&branch=main) [![Security](https://sonarcloud.io/api/project_badges/measure?project=CodeMaru-Dreamine_Dreamine.Database.Oracle&metric=security_rating&branch=main)](https://sonarcloud.io/summary/new_code?id=CodeMaru-Dreamine_Dreamine.Database.Oracle&branch=main) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=CodeMaru-Dreamine_Dreamine.Database.Oracle&metric=coverage&branch=main)](https://sonarcloud.io/summary/new_code?id=CodeMaru-Dreamine_Dreamine.Database.Oracle&branch=main)

[![라이선스](https://img.shields.io/github/license/CodeMaru-Dreamine/Dreamine.Database.Oracle?label=라이선스)](./LICENSE) [![.NET](https://img.shields.io/badge/.NET-8-512BD4)](https://dotnet.microsoft.com/) [![NuGet](https://img.shields.io/nuget/v/Dreamine.Database.Oracle?label=nuget)](https://www.nuget.org/packages/Dreamine.Database.Oracle) [![다운로드](https://img.shields.io/nuget/dt/Dreamine.Database.Oracle?label=다운로드)](https://www.nuget.org/packages/Dreamine.Database.Oracle)

[![문서](https://img.shields.io/badge/📘_문서-dreamine.kr-2F80ED)](https://dreamine.kr) [![가이드](https://img.shields.io/badge/📘_가이드-dreamine.kr-3498DB)](https://dreamine.kr) [![플레이그라운드](https://img.shields.io/badge/🎮_플레이그라운드-dreamine.kr-8E44AD)](https://dreamine.kr) [![도서](https://img.shields.io/badge/📖_도서-Practical_MVVM_Architecture-111111)](https://dreamine.kr)

`Dreamine.Database.Oracle`은 Dreamine Database 패키지군의 Oracle Provider입니다.

[English documentation / 영문 문서](./README.md)

## 패키지 역할

이 패키지는 `Oracle.ManagedDataAccess.Core`를 사용해서 Oracle용 `IDatabaseProvider`를 구현합니다.

```text
Dreamine.Database.Abstractions
        ↑
Dreamine.Database.Core
        ↑
Dreamine.Database.Oracle
```

## 주요 기능

- Oracle Connection 생성
- `:` 기반 Oracle Parameter Prefix 지원
- 대문자 Quoted Identifier
- Oracle Type Mapping
- Identity Column Table 생성
- 공통 `DatabaseProviderBase` 기반 CRUD

## 빠른 시작

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

## Schema 참고

Oracle은 보통 애플리케이션 Connection String으로 Database를 생성하기보다, 미리 만든 사용자/Schema에 접속하는 방식으로 사용합니다. 먼저 사용자/Schema를 준비하고 해당 계정에 Table 생성 권한을 부여하세요.

## 의존성

- `Dreamine.Database.Abstractions`
- `Dreamine.Database.Core`
- `Oracle.ManagedDataAccess.Core`

## 대상 프레임워크

```text
net8.0
```

## 샘플 및 테스트

- 단위 테스트: `20_SOURCES/200. Tests/Dreamine.FullKit.Tests/Database`
- WPF 샘플: `20_SOURCES/998. DEMO/000. Sample/010. Wpfs/SampleSmart/Pages/PageSub/PageDatabase.xaml`

## 라이선스

MIT License
