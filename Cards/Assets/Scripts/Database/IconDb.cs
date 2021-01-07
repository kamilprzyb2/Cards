using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
public class IconDb : SqliteHelper
{
	public IconDb() : base()
	{

	}
	public int Count()
	{
		SqliteCommand cmd = GetCommand();
		cmd.CommandText = "SELECT COUNT (*) FROM icon";
		SqliteDataReader reader = cmd.ExecuteReader();
		return reader.GetInt32(0);
	}

	public void Add(string name, string path, string note)
	{
		SqliteCommand cmd = GetCommand();
		cmd.CommandText = "INSERT INTO icon (name, note, path) VALUES (@name, @note, @path);";
		cmd.Parameters.AddWithValue("@name", name);
		cmd.Parameters.AddWithValue("@note", note);
		cmd.Parameters.AddWithValue("@path", path);

		cmd.Prepare();
		cmd.ExecuteNonQuery();
	}

	public void Update(int id, string name, string path, string note)
	{
		SqliteCommand cmd = GetCommand();
		cmd.CommandText = "UPDATE icon SET name = @name, path = @path, note = @note WHERE id = @id;";
		cmd.Parameters.AddWithValue("@name", name);
		cmd.Parameters.AddWithValue("@note", note);
		cmd.Parameters.AddWithValue("@path", path);
		cmd.Parameters.AddWithValue("@id", id);

		cmd.Prepare();
		cmd.ExecuteNonQuery();
	}

	public void DeleteById(int id)
	{
		SqliteCommand cmd = GetCommand();
		cmd.CommandText = "DELETE FROM icon WHERE id = @id;";
		cmd.Parameters.AddWithValue("@id", id);

		cmd.Prepare();
		cmd.ExecuteNonQuery();
	}

	public List<Icon> GetAll()
	{
		List<Icon> result = new List<Icon>();
		SqliteCommand cmd = GetCommand();
		cmd.CommandText =
			@"SELECT id, name, path, note
			FROM icon";
		SqliteDataReader reader = cmd.ExecuteReader();
		while (reader.Read())
		{
			int id = reader.GetInt32(0);
			string name = reader.GetString(1);
			string path = reader.GetString(2);
			string note = reader.GetString(3);
			result.Add(new Icon(id, name, path, note));
		}
		return result;
	}
}
