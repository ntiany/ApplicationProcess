using System.Collections.Generic;


namespace Model
{
    public interface IDaoPattern
    {
        List<List<string>> GetFilteredValues(string tableName, string condition, string conditionColumn, params string[] columnsNames);
        List<List<string>> GetColumnValues(string tableName, params string[] columnsNames);
        List<string> GetHeaders(string tableName);
        List<List<string>> GetValuesWithFullname(string tableName, string firstName, string lastName, string condition, string column);
        List<List<string>> GetValuesLikeWithFullname(string tableName, string firstName, string lastName, string condition, string conditionColumn, string column);
        void InsertRecord(string tableName, params string[] record);
        List<string> GetColumnsType(string tableName);
        int GetLastID(string tableName);
        bool IsDuplicated(string tableName, string columnName, int value);
        void UpdateRow(string tableName, int id, string columnName, string newValue);
        void DeleteRow(string tableName, int id);
    }

}
