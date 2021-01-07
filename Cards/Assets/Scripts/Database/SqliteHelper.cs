using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
public class SqliteHelper
{
    public SqliteConnection dbConnection;
    public string dbPath;
    
    /// <summary>
    /// Remember to call CreateTables in the editor
    /// </summary>
    public SqliteHelper()
    {
        // for editor work use path to assets, for release use appdata
        // TODO: have deck name as a setting for switching decks
        #if UNITY_EDITOR
                dbPath = "URI=file:" + Application.dataPath + "/Scripts/Database/deck.db";
        #else
                dbPath = "URI=file:" + Application.persistentDataPath + "/deck.db";
        #endif
        dbConnection = new SqliteConnection(dbPath);
        dbConnection.Open();

        // foreign keys are off by default
        SqliteCommand cmd = GetCommand();
        cmd.CommandText = "PRAGMA foreign_keys = ON;";
        cmd.ExecuteNonQuery();

        if (IsDbEmpty())
        {
            CreateTables();
            AddStartingGroup();
        }
    }
    ~SqliteHelper()
    {
        dbConnection.Close();
    }
    
    public SqliteCommand GetCommand()
    {
        return dbConnection.CreateCommand();
    }

    private void CreateTables()
    {
        SqliteCommand cmd = GetCommand();

        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS 'card_group' (
                            'id'    INTEGER,
	                        'name'  TEXT NOT NULL,
	                        'note'  TEXT,
	                        PRIMARY KEY('id' AUTOINCREMENT)); ";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS 'icon' (
                            'id'    INTEGER,
	                        'name'  TEXT NOT NULL,
                            'path'  TEXT NOT NULL,
	                        'note'  TEXT,
	                        PRIMARY KEY('id' AUTOINCREMENT)); ";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS 'card' (
							'id'    INTEGER,
							'group_id'  INTEGER NOT NULL,
							'name'  TEXT NOT NULL,
							'frequency' INTEGER NOT NULL,
							'decision1_name'    TEXT NOT NULL,
                            'decision1_description' TEXT NOT NULL,
                            'decision1_icon_id' INTEGER NOT NULL,
							'decision1_value1'  INTEGER NOT NULL,
							'decision1_value2'  INTEGER NOT NULL,
							'decision1_value3'  INTEGER NOT NULL,
							'decision1_value4'  INTEGER NOT NULL,
							'decision1_group_id'    INTEGER NOT NULL,
							'decision2_name'    TEXT NOT NULL,
							'decision2_value1'  INTEGER NOT NULL,
							'decision2_value2'  INTEGER NOT NULL,
							'decision2_value3'  INTEGER NOT NULL,
							'decision2_value4'  INTEGER NOT NULL,
                            'decision2_description' TEXT NOT NULL,
                            'decision2_icon_id' INTEGER NOT NULL,
							'decision2_group_id'    INTEGER NOT NULL,
							'type'  INTEGER NOT NULL,
							'note'  TEXT,
							PRIMARY KEY('id' AUTOINCREMENT),
							FOREIGN KEY('group_id') REFERENCES 'card_group'('id'),
							FOREIGN KEY('decision1_group_id') REFERENCES 'card_group'('id'),
							FOREIGN KEY('decision2_group_id') REFERENCES 'card_group'('id')); ";
        cmd.ExecuteNonQuery();
    }

    private bool IsDbEmpty()
    {
        SqliteCommand cmd = GetCommand();
        cmd.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_ % ';";
        SqliteDataReader reader = cmd.ExecuteReader();
        reader.Read();
        return (reader.GetInt32(0) == 0);
    }

    private void AddStartingGroup()
    {
        SqliteCommand cmd = GetCommand();
        cmd.CommandText = "INSERT INTO card_group (name, note) VALUES ('Start Group', 'Put all starter cards here');";
        cmd.ExecuteNonQuery();
    }
}
