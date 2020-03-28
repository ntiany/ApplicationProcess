using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class DaoDatabase : IDaoPattern
    {

        public List<List<string>> GetColumnValues(string tableName, params string[] columnsNames)
        {
            return DatabaseManager.GetColumnValues(tableName, columnsNames);
        }

        public List<List<string>> GetFilteredValues(string tableName, string condition, string conditionColumn, params string[] columnsNames)
        {
            return DatabaseManager.GetFilteredValues(tableName, condition, conditionColumn, columnsNames);
        }

        public List<string> GetHeaders(string tableName)
        {
            return DatabaseManager.GetHeaders(tableName);
        }

        public List<List<string>> GetValuesWithFullname(string tableName, string firstName, string lastName, string condition, string column)
        {
            return DatabaseManager.GetValuesWithFullname(tableName, firstName, lastName, condition, column);
        }

        public List<List<string>> GetValuesLikeWithFullname(string tableName, string firstName, string lastName, string condition, string conditionColumn, string column)
        {
            return DatabaseManager.GetValuesLikeWithFullname(tableName, firstName, lastName, condition, conditionColumn, column);
        }

        public void InsertRecord(string tableName, params string[] record)
        {
            DatabaseManager.InsertRow(tableName, record);
        }

        public List<string> GetColumnsType(string tableName)
        {
            return DatabaseManager.GetColumnsType(tableName);
        }

        public int GetLastID(string tableName)
        {
            return DatabaseManager.GetLastID(tableName);
        }

        public bool IsDuplicated(string tableName, string columnName, int value)
        {
            return DatabaseManager.IsDuplicated(tableName, columnName, value);
        }

        public void UpdateRow(string tableName, int ID, string columnName, string newValue)
        {
            DatabaseManager.UpdateRow(tableName, ID, columnName, newValue);
        }

        public void DeleteRow(string tableName, int id)
        {
            DatabaseManager.DeleteRow(tableName, id);
        }
    }
}