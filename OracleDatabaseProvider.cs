using Dreamine.Database.Abstractions;
using Dreamine.Database.Core.Mapping;
using Dreamine.Database.Core.Providers;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Dreamine.Database.Oracle;

/// <summary>
/// \if KO
/// <para>Oracle용 Dreamine 데이터베이스 공급자 구현을 제공합니다.</para>
/// \endif
/// \if EN
/// <para>Provides a Dreamine database-provider implementation for Oracle.</para>
/// \endif
/// </summary>
public sealed class OracleDatabaseProvider : DatabaseProviderBase
{
    /// <summary>
    /// \if KO
    /// <para>지정한 연결 문자열로 <see cref="OracleDatabaseProvider"/>의 새 인스턴스를 초기화합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Initializes a new <see cref="OracleDatabaseProvider"/> instance with the specified connection string.</para>
    /// \endif
    /// </summary>
    /// <param name="connectionString">
    /// \if KO
    /// <para>Oracle 연결 문자열입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The Oracle connection string.</para>
    /// \endif
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// \if KO
    /// <para><paramref name="connectionString"/>이 <see langword="null"/>인 경우 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown when <paramref name="connectionString"/> is <see langword="null"/>.</para>
    /// \endif
    /// </exception>
    /// <exception cref="ArgumentException">
    /// \if KO
    /// <para><paramref name="connectionString"/>이 비어 있거나 공백인 경우 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown when <paramref name="connectionString"/> is empty or white space.</para>
    /// \endif
    /// </exception>
    public OracleDatabaseProvider(string connectionString)
        : base(connectionString)
    {
    }

    /// <summary>
    /// \if KO
    /// <para>Oracle 공급자 종류를 가져옵니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets the Oracle provider kind.</para>
    /// \endif
    /// </summary>
    public override DatabaseProviderKind Kind => DatabaseProviderKind.Oracle;

    /// <summary>
    /// \if KO
    /// <para>Oracle 바인드 변수에 사용하는 콜론 접두사를 가져옵니다.</para>
    /// \endif
    /// \if EN
    /// <para>Gets the colon prefix used for Oracle bind variables.</para>
    /// \endif
    /// </summary>
    protected override string ParameterPrefix => ":";

    /// <summary>
    /// \if KO
    /// <para>현재 Oracle 스키마에 지정한 테이블이 존재하는지 확인합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Determines whether the specified table exists in the current Oracle schema.</para>
    /// \endif
    /// </summary>
    /// <param name="tableName">
    /// \if KO
    /// <para>확인할 테이블 이름입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The table name to inspect.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>테이블 존재 여부입니다.</para>
    /// \endif
    /// \if EN
    /// <para>Whether the table exists.</para>
    /// \endif
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// \if KO
    /// <para><paramref name="tableName"/>이 <see langword="null"/>인 경우 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown when <paramref name="tableName"/> is <see langword="null"/>.</para>
    /// \endif
    /// </exception>
    /// <exception cref="ArgumentException">
    /// \if KO
    /// <para><paramref name="tableName"/>이 비어 있거나 공백인 경우 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown when <paramref name="tableName"/> is empty or white space.</para>
    /// \endif
    /// </exception>
    public override bool IsTableExists(string tableName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(tableName);

        const string sql = """
            SELECT COUNT(1)
            FROM user_tables
            WHERE table_name = UPPER(:TableName)
            """;

        return ExecuteScalar<decimal>(sql, new { TableName = tableName }) > 0;
    }

    /// <summary>
    /// \if KO
    /// <para>현재 Oracle 스키마에 지정한 테이블이 존재하는지 비동기적으로 확인합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Asynchronously determines whether the specified table exists in the current Oracle schema.</para>
    /// \endif
    /// </summary>
    /// <param name="tableName">
    /// \if KO
    /// <para>확인할 테이블 이름입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The table name to inspect.</para>
    /// \endif
    /// </param>
    /// <param name="cancellationToken">
    /// \if KO
    /// <para>조회 취소 토큰입니다.</para>
    /// \endif
    /// \if EN
    /// <para>A token used to cancel the query.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>테이블 존재 여부를 결과로 제공하는 작업입니다.</para>
    /// \endif
    /// \if EN
    /// <para>A task whose result indicates whether the table exists.</para>
    /// \endif
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// \if KO
    /// <para><paramref name="tableName"/>이 <see langword="null"/>인 경우 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown when <paramref name="tableName"/> is <see langword="null"/>.</para>
    /// \endif
    /// </exception>
    /// <exception cref="ArgumentException">
    /// \if KO
    /// <para><paramref name="tableName"/>이 비어 있거나 공백인 경우 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown when <paramref name="tableName"/> is empty or white space.</para>
    /// \endif
    /// </exception>
    public override async Task<bool> IsTableExistsAsync(
        string tableName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(tableName);

        const string sql = """
            SELECT COUNT(1)
            FROM user_tables
            WHERE table_name = UPPER(:TableName)
            """;

        var count = await ExecuteScalarAsync<decimal>(sql, new { TableName = tableName }, cancellationToken)
            .ConfigureAwait(false);
        return count > 0;
    }

    /// <summary>
    /// \if KO
    /// <para>구성된 연결 문자열을 사용하는 새 Oracle 연결을 만듭니다.</para>
    /// \endif
    /// \if EN
    /// <para>Creates a new Oracle connection using the configured connection string.</para>
    /// \endif
    /// </summary>
    /// <returns>
    /// \if KO
    /// <para>닫힌 Oracle 연결입니다.</para>
    /// \endif
    /// \if EN
    /// <para>A closed Oracle connection.</para>
    /// \endif
    /// </returns>
    protected override IDbConnection CreateConnection()
    {
        return new OracleConnection(ConnectionString);
    }

    /// <summary>
    /// \if KO
    /// <para>Oracle 큰따옴표 문법으로 대문자 식별자를 안전하게 인용합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Safely quotes an upper-case identifier using Oracle double-quote syntax.</para>
    /// \endif
    /// </summary>
    /// <param name="identifier">
    /// \if KO
    /// <para>인용할 식별자입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The identifier to quote.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>이스케이프하고 인용한 식별자입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The escaped and quoted identifier.</para>
    /// \endif
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// \if KO
    /// <para><paramref name="identifier"/>가 <see langword="null"/>인 경우 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown when <paramref name="identifier"/> is <see langword="null"/>.</para>
    /// \endif
    /// </exception>
    /// <exception cref="ArgumentException">
    /// \if KO
    /// <para><paramref name="identifier"/>가 비어 있거나 공백인 경우 발생합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Thrown when <paramref name="identifier"/> is empty or white space.</para>
    /// \endif
    /// </exception>
    protected override string QuoteIdentifier(string identifier)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(identifier);
        return "\"" + identifier.Replace("\"", "\"\"", StringComparison.Ordinal).ToUpperInvariant() + "\"";
    }

    /// <summary>
    /// \if KO
    /// <para>IDENTITY 키를 지원하는 Oracle CREATE TABLE SQL을 만듭니다.</para>
    /// \endif
    /// \if EN
    /// <para>Builds Oracle CREATE TABLE SQL with identity-key support.</para>
    /// \endif
    /// </summary>
    /// <param name="map">
    /// \if KO
    /// <para>테이블 엔터티 매핑입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The table entity map.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>Oracle CREATE TABLE SQL입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The Oracle CREATE TABLE SQL.</para>
    /// \endif
    /// </returns>
    protected override string BuildCreateTableSql(DatabaseEntityMap map)
    {
        var columns = map.Properties.Select(property =>
        {
            var sql = $"{QuoteIdentifier(property.ColumnName)} {GetSqlType(property)}";
            if (property.IsKey)
            {
                sql += property.IsGenerated ? " GENERATED BY DEFAULT AS IDENTITY PRIMARY KEY" : " PRIMARY KEY";
            }

            return sql;
        });

        return $"CREATE TABLE {QuoteIdentifier(map.TableName)} ({string.Join(", ", columns)})";
    }

    /// <summary>
    /// \if KO
    /// <para>CLR 속성 형식을 대응하는 Oracle 열 형식으로 변환합니다.</para>
    /// \endif
    /// \if EN
    /// <para>Converts a CLR property type to its corresponding Oracle column type.</para>
    /// \endif
    /// </summary>
    /// <param name="property">
    /// \if KO
    /// <para>변환할 속성 매핑입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The property mapping to convert.</para>
    /// \endif
    /// </param>
    /// <returns>
    /// \if KO
    /// <para>Oracle 열 형식 선언입니다.</para>
    /// \endif
    /// \if EN
    /// <para>The Oracle column-type declaration.</para>
    /// \endif
    /// </returns>
    protected override string GetSqlType(DatabasePropertyMap property)
    {
        var type = property.PropertyType;

        if (type == typeof(bool) ||
            type == typeof(byte) ||
            type == typeof(short) ||
            type == typeof(int) ||
            type == typeof(long))
        {
            return "NUMBER";
        }

        if (type == typeof(float) ||
            type == typeof(double) ||
            type == typeof(decimal))
        {
            return "NUMBER(18, 4)";
        }

        if (type == typeof(DateTime) || type == typeof(DateTimeOffset))
        {
            return "TIMESTAMP";
        }

        if (type == typeof(byte[]))
        {
            return "BLOB";
        }

        return "NVARCHAR2(2000)";
    }
}
