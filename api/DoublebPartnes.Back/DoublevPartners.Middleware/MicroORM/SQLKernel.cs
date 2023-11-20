using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DoublebPartnes.Middleware.MicroORM;

public class SQLKernel
{
    private readonly string connectionString = string.Empty;

    public SQLKernel(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("MainConnection") ?? string.Empty;
    }

    private IDbConnection GetConnection()
    {
        var sqlConnection = new SqlConnection(connectionString);

        if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

        return sqlConnection;
    }

    /// <summary>
    ///     Ejecutar una sentencia Sql directo contra la base de datos
    /// </summary>
    /// <typeparam name="T">Tipo retornado por la ejecución del comando</typeparam>
    /// <param name="connection">Conexión a la base de datos</param>
    /// <param name="sql">Cadena con la sentencia sql</param>
    /// <param name="param">Objeto con los parámetros de la sentencia</param>
    /// <param name="transaction">Intancia de la transacción si es que exite</param>
    /// <param name="commandTimeout">Timeout del comando</param>
    /// <param name="commandType">Tipo de comando</param>
    /// <returns>Colección con los datos resultados de la consulta</returns>
    public IEnumerable<T> SqlQuery<T>(IDbConnection connection,
        string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        return connection.Query<T>(sql, param, transaction, true, commandTimeout, commandType);
    }


    /// <summary>
    ///     Ejecutar una sentecia sql directo contra la base de datos retornando una secuencia de objetos dinámicos
    ///     correspondiente a cada columna
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    public IEnumerable<dynamic> SqlQuery(string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        using var conn = GetConnection();
        return conn.Query(sql, param, transaction, true, commandTimeout, commandType);
    }

    /// <summary>
    ///     Ejecutar una sentencia Sql directo contra la base de datos
    /// </summary>
    /// <typeparam name="T">Tipo retornado por la ejecución del comando</typeparam>
    /// <param name="sql">Cadena con la sentencia sql</param>
    /// <param name="param">Objeto con los parámetros de la sentencia</param>
    /// <param name="transaction">Intancia de la transacción si es que exite</param>
    /// <param name="commandTimeout">Timeout del comando</param>
    /// <param name="commandType">Tipo de comando</param>
    /// <returns>Colección con los datos resultados de la consulta</returns>
    public IEnumerable<T> SqlQuery<T>(string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        using var conn = GetConnection();
        return conn.Query<T>(sql, param, transaction, true, commandTimeout, commandType);
    }

    /// <summary>
    ///     Ejecutar una sentencia Sql directo contra la base de datos
    /// </summary>
    /// <typeparam name="T">Tipo retornado por la ejecución del comando</typeparam>
    /// <param name="sql">Cadena con la sentencia sql</param>
    /// <param name="param">Objeto con los parámetros de la sentencia</param>
    /// <param name="transaction">Intancia de la transacción si es que exite</param>
    /// <param name="commandTimeout">Timeout del comando</param>
    /// <param name="commandType">Tipo de comando</param>
    /// <returns>Colección con los datos resultados de la consulta</returns>
    public async Task<T> QuerySingleAsync<T>(string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        using var conn = GetConnection();

        return await conn.QuerySingleAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }

    /// <summary>
    ///     Ejecutar una sentencia Sql directo contra la base de datos
    /// </summary>
    /// <typeparam name="T">Tipo retornado por la ejecución del comando</typeparam>
    /// <param name="sql">Cadena con la sentencia sql</param>
    /// <param name="param">Objeto con los parámetros de la sentencia</param>
    /// <param name="transaction">Intancia de la transacción si es que exite</param>
    /// <param name="commandTimeout">Timeout del comando</param>
    /// <param name="commandType">Tipo de comando</param>
    /// <returns>Colección con los datos resultados de la consulta</returns>
    public async Task<IEnumerable<T>> SqlQueryAsync<T>(string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        using var conn = GetConnection();
        return await conn.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }

    /// <summary>
    ///     Ejecutar un procedimiento almacenado directo contra la base de datos
    /// </summary>
    /// <typeparam name="T">Tipo retornado por la ejecución del comando</typeparam>
    /// <param name="sql">Cadena con la sentencia sql</param>
    /// <param name="param">Objeto con los parámetros de la sentencia</param>
    /// <param name="transaction">Intancia de la transacción si es que exite</param>
    /// <param name="commandTimeout">Timeout del comand (dado en segundos)o</param>
    /// <returns>Colección con los datos resultados de la consulta</returns>
    public IEnumerable<T> SqlQuerySP<T>(string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null)
    {
        using var conn = GetConnection();
        return conn.Query<T>($"call {sql}", param, transaction, true, commandTimeout);
    }

    /// <summary>
    ///     Ejecutar un procedimiento almacenado directo contra la base de datos
    /// </summary>
    /// <typeparam name="T">Tipo retornado por la ejecución del comando</typeparam>
    /// <param name="connection">Conexión a la base de datos</param>
    /// <param name="sql">Cadena con la sentencia sql</param>
    /// <param name="param">Objeto con los parámetros de la sentencia</param>
    /// <param name="transaction">Intancia de la transacción si es que exite</param>
    /// <param name="commandTimeout">Timeout del comando</param>
    /// <returns>Colección con los datos resultados de la consulta</returns>
    public IEnumerable<T> SqlQuerySP<T>(IDbConnection connection,
        string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null)
    {
        return connection.Query<T>($"call {sql}", param, transaction, true, commandTimeout);
    }


    /// <summary>
    ///     Ejecutar una sentencia Sql directo contra la base de datos
    /// </summary>
    /// <typeparam name="T">Tipo retornado por la ejecución del comando</typeparam>
    /// <param name="sql">Cadena con la sentencia sql</param>
    /// <param name="param">Objeto con los parámetros de la sentencia</param>
    /// <param name="transaction">Intancia de la transacción si es que exite</param>
    /// <param name="commandTimeout">Timeout del comando</param>
    /// <param name="commandType">Tipo de comando</param>
    /// <returns>El primer elementoo su valor por default si no retorna datos la consulta</returns>
    public T QueryFirstOrDefault<T>(string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        using var conn = GetConnection();
        return conn.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
    }

    /// <summary>
    ///     Ejecutar una sentencia Sql directo contra la base de datos de froma asíncrona
    /// </summary>
    /// <typeparam name="T">Tipo retornado por la ejecución del comando</typeparam>
    /// <param name="sql">Cadena con la sentencia sql</param>
    /// <param name="param">Objeto con los parámetros de la sentencia</param>
    /// <param name="transaction">Intancia de la transacción si es que exite</param>
    /// <param name="commandTimeout">Timeout del comando</param>
    /// <param name="commandType">Tipo de comando</param>
    /// <returns>El primer elementoo su valor por default si no retorna datos la consulta</returns>
    public async Task<T> QueryFirstOrDefaultAsync<T>(string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        using var conn = GetConnection();

        return await conn.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }

    /// <summary>
    ///     Ejecutar una sentencia Sql directo contra la base de datos
    /// </summary>
    /// <typeparam name="T">Tipo retornado por la ejecución del comando</typeparam>
    /// <param name="connection">Conexión a la base de datos</param>
    /// <param name="sql">Cadena con la sentencia sql</param>
    /// <param name="param">Objeto con los parámetros de la sentencia</param>
    /// <param name="transaction">Intancia de la transacción si es que exite</param>
    /// <param name="commandTimeout">Timeout del comando</param>
    /// <param name="commandType">Tipo de comando</param>
    /// <returns>El primer elementoo su valor por default si no retorna datos la consulta</returns>
    public T QueryFirstOrDefault<T>(IDbConnection connection,
        string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        return connection.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
    }

    /// <summary>
    ///     Ejecutar un comando Sql directo contra la base de datos
    /// </summary>
    /// <param name="sql">Cadena con la sentencia sql</param>
    /// <param name="param">Objeto con los parámetros de la sentencia</param>
    /// <param name="transaction">Intancia de la transacción si es que exite</param>
    /// <param name="commandTimeout">Timeout del comando</param>
    /// <param name="commandType">Tipo de comando</param>
    /// <returns>Cantidad de filas afectadas por el comando</returns>
    public int Execute(string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        using var conn = GetConnection();
        return conn.Execute(sql, param, transaction, commandTimeout, commandType);
    }

    /// <summary>
    ///     Ejecutar un comando Sql directo contra la base de datos de forma asíncrona
    /// </summary>
    /// <param name="sql">Cadena con la sentencia sql</param>
    /// <param name="param">Objeto con los parámetros de la sentencia</param>
    /// <param name="transaction">Intancia de la transacción si es que exite</param>
    /// <param name="commandTimeout">Timeout del comando</param>
    /// <param name="commandType">Tipo de comando</param>
    /// <returns>Cantidad de filas afectadas por el comando</returns>
    public async Task<int> ExecuteAsync(string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        using var conn = GetConnection();

        return await conn.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
    }

    /// <summary>
    ///     Ejecutar un procedimiento almacenado directo contra la base de datos
    /// </summary>
    /// <param name="sql">Cadena con la sentencia sql</param>
    /// <param name="param">Objeto con los parámetros de la sentencia</param>
    /// <param name="transaction">Intancia de la transacción si es que exite</param>
    /// <param name="commandTimeout">Timeout del comando</param>
    /// <returns>Cantidad de filas afectadas por el comando</returns>
    public int ExecuteSP(string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null)
    {
        using var conn = GetConnection();
        return conn.Execute($"call {sql}", param, transaction, commandTimeout);
    }

    /// <summary>
    ///     Ejecutar un procedimiento almacenado directo contra la base de datos
    /// </summary>
    /// <param name="connection">Conexión a la base de datos</param>
    /// <param name="sql">Cadena con la sentencia sql</param>
    /// <param name="param">Objeto con los parámetros de la sentencia</param>
    /// <param name="transaction">Intancia de la transacción si es que exite</param>
    /// <param name="commandTimeout">Timeout del comando</param>
    /// <returns>Cantidad de filas afectadas por el comando</returns>
    public int ExecuteSP(IDbConnection connection,
        string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null)
    {
        return connection.Execute($"call {sql}", param, transaction, commandTimeout);
    }

    /// <summary>
    ///     Ejecutar un comando Sql directo contra la base de datos
    /// </summary>
    /// <param name="connection">Conexión a la base de datos</param>
    /// <param name="sql">Cadena con la sentencia sql</param>
    /// <param name="param">Objeto con los parámetros de la sentencia</param>
    /// <param name="transaction">Intancia de la transacción si es que exite</param>
    /// <param name="commandTimeout">Timeout del comando en segundos</param>
    /// <param name="commandType">Tipo de comando</param>
    /// <returns>Cantidad de filas afectadas por el comando</returns>
    public int Execute(IDbConnection connection,
        string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        return connection.Execute(sql, param, transaction, commandTimeout, commandType);
    }

    /// <summary>
    ///     Ejecutar un comando Sql directo contra la base de datos de forma asíncrona
    /// </summary>
    /// <param name="connection">Conexión a la base de datos</param>
    /// <param name="sql">Cadena con la sentencia sql</param>
    /// <param name="param">Objeto con los parámetros de la sentencia</param>
    /// <param name="transaction">Intancia de la transacción si es que exite</param>
    /// <param name="commandTimeout">Timeout del comando en segundos</param>
    /// <param name="commandType">Tipo de comando</param>
    /// <returns>Cantidad de filas afectadas por el comando</returns>
    public async Task<int> ExecuteAsync(IDbConnection connection,
        string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        return await connection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
    }

    /// <summary>
    ///     Ejecutar un comando Sql directo contra la base de datos y retorna el campo de la primera fila y primera columna
    /// </summary>
    /// <typeparam name="T">Tipo retornado por la ejecución del comando</typeparam>
    /// <param name="sql">Cadena con la sentencia sql</param>
    /// <param name="param">Objeto con los parámetros de la sentencia</param>
    /// <param name="transaction">Intancia de la transacción si es que exite</param>
    /// <param name="commandTimeout">Timeout del comando</param>
    /// <param name="commandType">Tipo de comando</param>
    /// <returns>Valor de la primera fila y primera columna</returns>
    public T ExecuteScalar<T>(string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        using var conn = GetConnection();
        return conn.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
    }


    /// <summary>
    ///     Ejecutar un comando Sql directo contra la base de datos y retorna el campo de la primera fila y primera columna
    /// </summary>
    /// <typeparam name="T">Tipo retornado por la ejecución del comando</typeparam>
    /// <param name="connection">Conexión a la base de datos</param>
    /// <param name="sql">Cadena con la sentencia sql</param>
    /// <param name="param">Objeto con los parámetros de la sentencia</param>
    /// <param name="transaction">Intancia de la transacción si es que exite</param>
    /// <param name="commandTimeout">Timeout del comando</param>
    /// <param name="commandType">Tipo de comando</param>
    /// <returns>Valor de la primera fila y primera columna</returns>
    public T ExecuteScalar<T>(IDbConnection connection,
        string sql,
        object param = null,
        IDbTransaction transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        return connection.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
    }
}