using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
public class CardDb : SqliteHelper
{

	public CardDb() : base()
	{
		
	}

	public int Count()
    {
		SqliteCommand cmd = GetCommand();
		cmd.CommandText = "SELECT COUNT (*) FROM card";
		SqliteDataReader reader = cmd.ExecuteReader();
		return reader.GetInt32(0);
	}
	public void Add(CardModel model)
    {
		SqliteCommand cmd = GetCommand();
		cmd.CommandText =
			@"INSERT INTO card (group_id, icon_id, name, description, frequency,
			decision1_name, decision2_name,
			decision1_value1, decision1_value2, decision1_value3, decision1_value4,
			decision2_value1, decision2_value2, decision2_value3, decision2_value4,
			decision1_group_id, decision2_group_id, type, note)
			VALUES (@group_id, @icon_id, @name, @description, @frequency,
			@decision1_name, @decision2_name,
			@decision1_value1, @decision1_value2, @decision1_value3, @decision1_value4,
			@decision2_value1, @decision2_value2, @decision2_value3, @decision2_value4,
			@decision1_group_id, @decision2_group_id, @type, @note);";
		cmd.Parameters.AddWithValue("@group_id", model.GroupId);
		cmd.Parameters.AddWithValue("@icon_id", model.IconId);
		cmd.Parameters.AddWithValue("@name", model.Name);
		cmd.Parameters.AddWithValue("@description", model.Description);
		cmd.Parameters.AddWithValue("@frequency", model.Frequency);
		cmd.Parameters.AddWithValue("@decision1_name", model.Decisions[0].Name);
		cmd.Parameters.AddWithValue("@decision2_name", model.Decisions[1].Name);
		cmd.Parameters.AddWithValue("@decision1_value1", model.Decisions[0].Values[0]);
		cmd.Parameters.AddWithValue("@decision1_value2", model.Decisions[0].Values[1]);
		cmd.Parameters.AddWithValue("@decision1_value3", model.Decisions[0].Values[2]);
		cmd.Parameters.AddWithValue("@decision1_value4", model.Decisions[0].Values[3]);
		cmd.Parameters.AddWithValue("@decision2_value1", model.Decisions[1].Values[0]);
		cmd.Parameters.AddWithValue("@decision2_value2", model.Decisions[1].Values[1]);
		cmd.Parameters.AddWithValue("@decision2_value3", model.Decisions[1].Values[2]);
		cmd.Parameters.AddWithValue("@decision2_value4", model.Decisions[1].Values[3]);
		cmd.Parameters.AddWithValue("@decision1_group_id", model.Decisions[0].GroupId);
		cmd.Parameters.AddWithValue("@decision2_group_id", model.Decisions[1].GroupId);
		cmd.Parameters.AddWithValue("@type", model.Type);
		cmd.Parameters.AddWithValue("@note", model.Note);

		cmd.Prepare();
		cmd.ExecuteNonQuery();
	}
	public List<Card> GetAll()
    {
		SqliteCommand cmd = GetCommand();
		cmd.CommandText =
			@"SELECT id, group_id, icon_id, name, description, frequency,
			decision1_name, decision2_name,
			decision1_value1, decision1_value2, decision1_value3, decision1_value4,
			decision2_value1, decision2_value2, decision2_value3, decision2_value4,
			decision1_group_id, decision2_group_id, type, note
			FROM card";
		return Get(cmd);
	}
	public List<Card> GetByGroup(int groupId)
	{
		SqliteCommand cmd = GetCommand();
		cmd.CommandText =
			@"SELECT id, group_id, icon_id, name, description, frequency,
			decision1_name, decision2_name,
			decision1_value1, decision1_value2, decision1_value3, decision1_value4,
			decision2_value1, decision2_value2, decision2_value3, decision2_value4,
			decision1_group_id, decision2_group_id, type, note
			FROM card
			WHERE group_id = @group_id
			";
		cmd.Parameters.AddWithValue("@group_id", groupId);
		cmd.Prepare();
		return Get(cmd);
	}
	public Card GetById(int id)
    {
		SqliteCommand cmd = GetCommand();
		cmd.CommandText =
			@"SELECT id, group_id, icon_id, name, description, frequency,
			decision1_name, decision2_name,
			decision1_value1, decision1_value2, decision1_value3, decision1_value4,
			decision2_value1, decision2_value2, decision2_value3, decision2_value4,
			decision1_group_id, decision2_group_id, type, note
			FROM card WHERE id = @id;";
		cmd.Parameters.AddWithValue("@id", id);
		cmd.Prepare();
		SqliteDataReader reader = cmd.ExecuteReader();
		reader.Read();
		return Get(reader);
	}
	private List<Card> Get(SqliteCommand cmd)
    {
		List<Card> result = new List<Card>();
		SqliteDataReader reader = cmd.ExecuteReader();
		while (reader.Read())
		{
			result.Add(Get(reader));
		}
		return result;
	}
	private Card Get(SqliteDataReader reader)
    {
		CardModel model = new CardModel();
		model.Id = reader.GetInt32(0);
		model.GroupId = reader.GetInt32(1);
		model.IconId = reader.GetInt32(2);
		model.Name = reader.GetString(3);
		model.Description = reader.GetString(4);
		model.Frequency = reader.GetInt32(5);
		model.Decisions[0].Name = reader.GetString(6);
		model.Decisions[1].Name = reader.GetString(7);
		for (int i = 0; i < 8; i++)
		{
			model.Decisions[i / 4].Values[i % 4] = reader.GetInt32(8 + i);
		}
		model.Decisions[0].GroupId = reader.GetInt32(16);
		model.Decisions[1].GroupId = reader.GetInt32(17);
		model.Type = reader.GetInt32(18);
		model.Note = reader.GetString(19);

		return new Card(model);
	}
}


