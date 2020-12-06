using Mono.Data.Sqlite;
using System.Data.Common;
using System.Linq;
using UnityEngine;

class ColumnReader
{
    SqliteDataReader reader;

    public ColumnReader(SqliteDataReader Reader)
    {
        reader = Reader;
    }

    public string GetString(string ColumnName)
    {
        int index = -1;
        for (var i = 0; i < reader.FieldCount; i++)
        {
            if (reader.GetName(i) == ColumnName)
            {
                index = i;
                break;
            }
        }

        if (index > -1)
        {
            return reader.GetString(index);
        }
        else
        {
            return null;
        }
    }

    public int GetInt(string ColumnName)
    {
        int index = -1;
        for (var i = 0; i < reader.FieldCount; i++)
        {
            if (reader.GetName(i) == ColumnName)
            {
                index = i;
                break;
            }
        }

        if (index > -1)
        {
            return reader.GetInt32(index);
        }
        else
        {
            return 0;
        }
    }
}