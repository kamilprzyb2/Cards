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


	public int GetCardCount()
    {
		IDbCommand cmd = GetCommand();
		cmd.CommandText = "SELECT COUNT (*) FROM card";
		IDataReader reader = cmd.ExecuteReader();
		int result;
		int.TryParse(reader[0].ToString(), out result);
		return result;
	}
}


