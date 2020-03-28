using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Model
{
    internal static class DatabaseManager 
    {
        public static List<List<string>> GetValuesWithFullname(string tableName, string firstName, string lastName, string condition, string column)
        {
            List<List<string>> data = new List<List<string>>();
            string sql = $"SELECT {firstName} || ' ' || {lastName} AS full_name, {column} FROM {tableName} WHERE {column} = {condition}";
            return HandleOutputCommand(sql, true);
        }

        public static List<List<string>> GetValuesLikeWithFullname(string tableName, string firstName, string lastName, string condition, string conditionColumn, string column)
        {
            List<List<string>> data = new List<List<string>>();
            string sql = $"SELECT {firstName} || ' ' || {lastName} AS full_name, {column} FROM {tableName} WHERE {conditionColumn} LIKE {condition}";
            return HandleOutputCommand(sql, true);
        }


        public static List<List<string>> GetColumnValues(string tableName, params string[] columns)
        {
            string columnsTogether = AdjustColumnsName(columns);
            string sql = $"SELECT {columnsTogether} FROM {tableName}";
            return HandleOutputCommand(sql, false);
        }

        public static List<List<string>> GetFilteredValues(string tableName, string condition, string conditionColumn, params string[] columns)
        {
            string columnsTogether = AdjustColumnsName(columns);
            string sql = $"SELECT {columnsTogether} FROM {tableName} WHERE {conditionColumn} = {condition}";
            return HandleOutputCommand(sql, false);
        }

        public static List<string> GetHeaders(string tableName)
        {
            string sql = $"SELECT * FROM {tableName} WHERE false";

            using NpgsqlConnection connection = CreateConnection();
            connection.Open();

            using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            using NpgsqlDataReader reader = command.ExecuteReader();
            int fieldCount = reader.FieldCount;

            List<string> row = new List<string>();
            for (int i = 0; i < fieldCount; i++)
            {
                row.Add(reader.GetName(i));
            }

            return row;
        }

        public static List<string> GetColumnsType(string tableName)
        {
            using NpgsqlConnection connection = CreateConnection();
            connection.Open();
            string sql = $"SELECT data_type FROM information_schema.columns WHERE table_name = '{tableName}'";
            using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            using NpgsqlDataReader reader = command.ExecuteReader();
            int fieldCount = reader.FieldCount;

            List<string> type = new List<string>();

            while (reader.Read())
            {
                if (reader.GetString(0) == "integer") type.Add("int");
                else type.Add("str");    
            }

            return type;
        }

        public static int GetLastID(string tableName)
        {
            using NpgsqlConnection connection = CreateConnection();
            connection.Open();
            string sql = $"SELECT id FROM {tableName} ORDER BY id DESC LIMIT 1";
            using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            using NpgsqlDataReader reader = command.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0);
        }

        public static bool IsDuplicated(string tableName, string columnName, int value)
        {
            using NpgsqlConnection connection = CreateConnection();
            connection.Open();
            string sql = $"SELECT * FROM {tableName} WHERE {columnName} = {value}";
            using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            using NpgsqlDataReader reader = command.ExecuteReader();

            return reader.Read();
        }

        public static void InsertRow(string tableName, string[] values)
        {
            string sql = $"INSERT INTO {tableName} VALUES ({string.Join(',', values)})";
            ExecuteNonQuery(sql);
        }

        public static void UpdateRow(string tableName, int id, string column, string value)
        {
            string sql = $"UPDATE {tableName} SET {column} = {value} WHERE id = {id}";
            ExecuteNonQuery(sql);
        }

        public static void DeleteRow(string tableName, int id)
        {
            string sql = $"DELETE FROM {tableName} WHERE id = {id}";
            ExecuteNonQuery(sql);
        }

        private static void ExecuteNonQuery(string sql)
        {
            using NpgsqlConnection connection = CreateConnection();
            connection.Open();

            using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }


        private static List<List<string>> HandleOutputCommand(string sql, bool includeHeaders)
        {
            List<List<string>> data = new List<List<string>>();

            using NpgsqlConnection connection = CreateConnection();
            connection.Open();

            using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            using NpgsqlDataReader reader = command.ExecuteReader();
            int fieldCount = reader.FieldCount;

            if (includeHeaders)
            {
                List<string> row = new List<string>();
                for (int i = 0; i < fieldCount; i++)
                {
                    row.Add(reader.GetName(i));
                }
                data.Add(row);
            }

            while (reader.Read())
            {
                List<string> row = new List<string>();
                for (int i = 0; i < fieldCount; i++)
                {
                    if (reader.GetPostgresType(i).Name == "integer")
                        row.Add($"{reader.GetInt32(i)}");
                    else row.Add(reader.GetString(i));  
                }
                data.Add(row);
            }
            return data;
        }
          
        private static string AdjustColumnsName(string[] columns)
        {
            return string.Join(',', columns);
        }

        private static NpgsqlConnection CreateConnection()
        {
            // add app.config file with connection string to solution to connect to your database
            string connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            return connection;

        }
    }
}
