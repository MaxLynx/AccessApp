using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessApp
{
    class DAO
    {
        String connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Database.accdb";
        public List<string> ListboxItems = new List<string>();
        public DataTable GetAllTables()
        {
            DataTable userTables;
            using (OleDbConnection connection = new OleDbConnection(connString))
            {
                connection.Open();
                string[] restrictions = new string[4];
                restrictions[3] = "Table";
                userTables = connection.GetSchema("Tables", restrictions);
                return userTables;

            }
        }

        public DataTable GetTableByName(String tableName)
        {
            DataTable table = new DataTable();
            using (OleDbConnection connection = new OleDbConnection(connString))
            {
                if (tableName.Contains(" "))
                    tableName = "[" + tableName + "]";
                connection.Open();
                OleDbDataReader reader = null;
                OleDbCommand command = new OleDbCommand("SELECT * from  " + tableName, connection);
                reader = command.ExecuteReader();
                Boolean firstRow = true;
                while (reader.Read())
                {
                    if (firstRow)
                    {
                        List<String> columnNames = GetColumnNamesForTable(tableName);
                        foreach (String columnName in columnNames)
                            table.Columns.Add(columnName, typeof(string));
                    }
                    firstRow = false;
                    String[] row = new String[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                        row[i] = reader[i].ToString();
                    table.Rows.Add(row);
                }
                return table;
            }
        }

        public List<String> GetColumnNamesForTable(String tableName)
        {
            List<String> columnNames = new List<String>();
            using (var con = new OleDbConnection(connString))
            {
                con.Open();
                using (var cmd = new OleDbCommand("select * from " + tableName, con))
                using (var reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly))
                {
                    var table = reader.GetSchemaTable();
                    var nameCol = table.Columns["ColumnName"];
                    foreach (DataRow row in table.Rows)
                    {
                        columnNames.Add(row[nameCol].ToString());
                    }
                }
                return columnNames;
            }
        }

        public void DeleteFromTableById(String tableName, String id)
        {
            DataTable table = GetTableByName(tableName);
            if(table.Columns[0].DataType == typeof(string))
            {
                id = "'" + id + "'";
            }
            using (OleDbConnection connection = new OleDbConnection(connString))
            {
                if (tableName.Contains(" "))
                    tableName = "[" + tableName + "]";
                connection.Open();
                String idColumnName = GetColumnNamesForTable(tableName)[0];
                if (idColumnName.Contains(" "))
                    idColumnName = "[" + idColumnName + "]";
                OleDbCommand command = new OleDbCommand("DELETE from " + tableName + " " +
                    "WHERE " + idColumnName + " = " + id, connection);
                command.ExecuteNonQuery();
            }
        }

        public void InsertToTable(String tableName, List<String> tableNames)
        {
            DataTable table = GetTableByName(tableName);
            if (tableName.Contains(" "))
                tableName = "[" + tableName + "]";
            String insertionString = "INSERT INTO " + tableName + " VALUES(";
            for (int i = 0; i < tableNames.Count; i++)
            {
                if (table.Columns[i].DataType == typeof(string))
                {
                    insertionString += "'" + tableNames[i] + "'";
                }
                else
                {
                    insertionString += tableNames[i];
                }
                if (i != tableNames.Count - 1)
                    insertionString += ", ";
                else
                    insertionString += ")";
            }
            using (OleDbConnection connection = new OleDbConnection(connString))
            {
                
                connection.Open();
                
                OleDbCommand command = new OleDbCommand(insertionString, connection);
                command.ExecuteNonQuery();
            }
        }

        public List<String> GetWarriors()
        {
            List<String> warriors = new List<String>();
            DataTable table = new DataTable();
            using (OleDbConnection connection = new OleDbConnection(connString))
            {
                
                connection.Open();
                OleDbDataReader reader = null;
                String query = "SELECT [Загальні відомості].Прізвище, [Загальні відомості].[Ім'я], " +
                    "[Загальні відомості].[По батькові], [Загальні відомості].[Дата народження] FROM [Загальні відомості]" +
                    " INNER JOIN [Військовий облік] ON [Загальні відомості].[Табельний номер] = " +
                    "[Військовий облік].[Табельний номер] " +
                    "WHERE [Військовий облік].Придатність = 'Придатний' OR" +
                    " [Військовий облік].Придатність = 'Придатна'";
                OleDbCommand command = new OleDbCommand(query, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    String row = "";
                    for (int i = 0; i < reader.FieldCount; i++)
                        if (i != reader.FieldCount - 1)
                            row += reader[i].ToString() + " ";
                        else
                            row += reader[i].ToString().Substring(0, 10);
                    warriors.Add(row);
                }
                return warriors;
            }
        }

        public List<String> GetLonelyUkrainians()
        {
            List<String> lonelyCossacks = new List<String>();
            DataTable table = new DataTable();
            using (OleDbConnection connection = new OleDbConnection(connString))
            {

                connection.Open();
                OleDbDataReader reader = null;
                String query = "SELECT [Загальні відомості].Прізвище, [Загальні відомості].[Ім'я], " +
                    "[Загальні відомості].[По батькові], [Загальні відомості].[Дата народження] FROM [Загальні відомості]" +
                    " LEFT JOIN [Склад сім'ї] ON [Загальні відомості].[Табельний номер] = " +
                    "[Склад сім'ї].[Табельний номер] " +
                    "WHERE [Склад сім'ї].[Табельний номер] IS NULL";
                
                    
                OleDbCommand command = new OleDbCommand(query, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    String row = "";
                    for (int i = 0; i < reader.FieldCount; i++)
                            row += reader[i].ToString() + " ";
                    lonelyCossacks.Add(row);
                }
                return lonelyCossacks;
            }
        }

        public List<String> GetYoungsters()
        {
            List<String> youngsters = new List<String>();
            DataTable table = new DataTable();
            using (OleDbConnection connection = new OleDbConnection(connString))
            {

                connection.Open();
                OleDbDataReader reader = null;
                String query = "SELECT Прізвище, [Ім'я], " +
                    "[По батькові], [Дата народження] FROM [Загальні відомості]" +                    
                    "WHERE [Дата народження] > #" + "31/12/1992" + "# ";
                OleDbCommand command = new OleDbCommand(query, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    String row = "";
                    for (int i = 0; i < reader.FieldCount; i++)
                        if (i != reader.FieldCount - 1)
                            row += reader[i].ToString() + " ";
                        else
                            row += reader[i].ToString().Substring(0, 10);
                    youngsters.Add(row);
                }
                return youngsters;
            }
        }
    }
}
