using System;
using SQLite.Net;
//Local Database/storage
namespace CatchUp.Core.Interfaces
{
	public interface ISqlite
	{
		SQLiteConnection GetConnection();

	}
}

