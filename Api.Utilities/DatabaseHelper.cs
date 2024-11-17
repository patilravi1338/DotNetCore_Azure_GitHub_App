using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Api.Utilities
{
  public class DatabaseHelper : IDisposable
  {
    private readonly string _connectionString;
    private SqlConnection _connection;

    public DatabaseHelper(IConfiguration configuration)
    {
      _connectionString = configuration.GetConnectionString("DefaultConnection");
      _connection = new SqlConnection(_connectionString);
    }

    // Open database connection
    private void OpenConnection()
    {
      if (_connection.State != ConnectionState.Open)
        _connection.Open();
    }

    // Close database connection
    private void CloseConnection()
    {
      if (_connection.State == ConnectionState.Open)
        _connection.Close();
    }

    /// <summary>
    /// Executes a SQL query or stored procedure and returns a DataTable.
    /// </summary>
    public DataTable ExecuteQuery(string query, CommandType commandType, Dictionary<string, object> parameters = null)
    {
      var dataTable = new DataTable();

      using (var command = new SqlCommand(query, _connection))
      {
        command.CommandType = commandType;

        if (parameters != null)
        {
          foreach (var param in parameters)
          {
            command.Parameters.AddWithValue(param.Key, param.Value);
          }
        }

        OpenConnection();
        using (var adapter = new SqlDataAdapter(command))
        {
          adapter.Fill(dataTable);
        }
        CloseConnection();
      }

      return dataTable;
    }

    /// <summary>
    /// Executes a SQL query or stored procedure for Insert, Update, or Delete.
    /// </summary>
    public int ExecuteNonQuery(string query, CommandType commandType, Dictionary<string, object> parameters = null)
    {
      using (var command = new SqlCommand(query, _connection))
      {
        command.CommandType = commandType;

        if (parameters != null)
        {
          foreach (var param in parameters)
          {
            command.Parameters.AddWithValue(param.Key, param.Value);
          }
        }

        OpenConnection();
        int affectedRows = command.ExecuteNonQuery();
        CloseConnection();

        return affectedRows;
      }
    }

    /// <summary>
    /// Executes a SQL query or stored procedure and returns a scalar value.
    /// </summary>
    public object ExecuteScalar(string query, CommandType commandType, Dictionary<string, object> parameters = null)
    {
      using (var command = new SqlCommand(query, _connection))
      {
        command.CommandType = commandType;

        if (parameters != null)
        {
          foreach (var param in parameters)
          {
            command.Parameters.AddWithValue(param.Key, param.Value);
          }
        }

        OpenConnection();
        object result = command.ExecuteScalar();
        CloseConnection();

        return result;
      }
    }

    // Implement IDisposable to handle resource cleanup
    public void Dispose()
    {
      if (_connection != null)
      {
        _connection.Dispose();
        _connection = null;
      }
    }
  }
}
