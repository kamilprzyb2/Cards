using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
public class GroupDb : SqliteHelper
{
    public GroupDb() : base()
    {

    }
	public int Count()
	{
		SqliteCommand cmd = GetCommand();
		cmd.CommandText = "SELECT COUNT (*) FROM card_group";
		SqliteDataReader reader = cmd.ExecuteReader();
		return reader.GetInt32(0);
	}

	public void Add(string name, string note)
	{
		SqliteCommand cmd = GetCommand();
		cmd.CommandText = "INSERT INTO card_group (name, note) VALUES (@name, @note);";
		cmd.Parameters.AddWithValue("@name", name);
		cmd.Parameters.AddWithValue("@note", note);

		cmd.Prepare();
		cmd.ExecuteNonQuery();
	}

	public void Update(int id, string name, string note)
    {
		SqliteCommand cmd = GetCommand();
		cmd.CommandText = "UPDATE card_group SET name = @name,  note = @note WHERE id = @id;";
		cmd.Parameters.AddWithValue("@name", name);
		cmd.Parameters.AddWithValue("@note", note);
		cmd.Parameters.AddWithValue("@id", id);

		cmd.Prepare();
		cmd.ExecuteNonQuery();
	}

	public void DeleteById(int id)
    {
		SqliteCommand cmd = GetCommand();
		cmd.CommandText = "DELETE FROM card_group WHERE id = @id;";
		cmd.Parameters.AddWithValue("@id", id);

		cmd.Prepare();
		cmd.ExecuteNonQuery();
	}

	public List<Group> GetAll()
	{
		List<Group> result = new List<Group>();
		SqliteCommand cmd = GetCommand();
		cmd.CommandText =
			@"SELECT id, name, note
			FROM card_group";
		SqliteDataReader reader = cmd.ExecuteReader();
		while (reader.Read())
		{
			int id = reader.GetInt32(0);
			string name = reader.GetString(1);
			string note = reader.GetString(2);
			result.Add(new Group(id, name, note));
		}
		return result;
	}
}
