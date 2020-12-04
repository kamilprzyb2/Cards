using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
public class SqliteHelper
{
    public IDbConnection dbConnection;
    public string dbPath;
    
    /// <summary>
    /// Remember to call CreateTables when application starts!
    /// </summary>
    public SqliteHelper()
    {
        // for editor work use path to assets, for release use appdata
        #if UNITY_EDITOR
                dbPath = "URI=file:" + Application.dataPath + "/Scripts/Database/deck.db";
        #else
                dbPath = "URI=file:" + Application.persistentDataPath + "/deck.db";
        #endif
        dbConnection = new SqliteConnection(dbPath);
        dbConnection.Open();
    }
    ~SqliteHelper()
    {
        dbConnection.Close();
    }
    
    public IDbCommand GetCommand()
    {
        return dbConnection.CreateCommand();
    }
    public virtual IDataReader GetAll()
    {
        Debug.LogError("Unimplemented function");
        throw null;
    }

    public virtual IDataReader GetById(int id)
    {
        Debug.LogError("Unimplemented function");
        throw null;
    }

    public virtual void DeleteById()
    {
        Debug.LogError("Unimplemented function");
        throw null;
    }

    public void CreateTables()
    {
        IDbCommand cmd = GetCommand();

        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS 'group' (
                            'id'    INTEGER,
	                        'name'  TEXT NOT NULL,
	                        'note'  TEXT,
	                        PRIMARY KEY('id' AUTOINCREMENT)); ";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS 'icon' (
                            'id'    INTEGER,
	                        'name'  TEXT NOT NULL,
	                        'note'  TEXT,
	                        PRIMARY KEY('id' AUTOINCREMENT)); ";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS 'card' (
							'id'    INTEGER,
							'group_id'  INTEGER,
							'name'  TEXT NOT NULL,
							'description'   TEXT NOT NULL,
							'frequency' INTEGER NOT NULL,
							'decision1_name'    TEXT NOT NULL,
							'decision2_name'    TEXT NOT NULL,
							'decision1_value1'  INTEGER NOT NULL,
							'decision1_value2'  INTEGER NOT NULL,
							'decision1_value3'  INTEGER NOT NULL,
							'decision1_value4'  INTEGER NOT NULL,
							'decision2_value1'  INTEGER NOT NULL,
							'decision2_value2'  INTEGER NOT NULL,
							'decision2_value3'  INTEGER NOT NULL,
							'decision2_value4'  INTEGER NOT NULL,
							'decision1_group_id'    INTEGER,
							'decision2_group_id'    INTEGER,
							'type'  INTEGER NOT NULL,
							'note'  TEXT,
							PRIMARY KEY('id' AUTOINCREMENT),
							FOREIGN KEY('group_id') REFERENCES 'group'('id'),
							FOREIGN KEY('decision1_group_id') REFERENCES 'group'('id'),
							FOREIGN KEY('decision2_group_id') REFERENCES 'group'('id')); ";
        cmd.ExecuteNonQuery();
    }
}
