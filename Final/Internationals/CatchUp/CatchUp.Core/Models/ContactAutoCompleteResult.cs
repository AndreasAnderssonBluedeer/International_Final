using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
namespace CatchUp.Core.Models
{
	public class ContactAutoCompleteResult
	{
		//[PrimaryKey, AutoIncrement]
		public string FirstName { get; set; }
		public string LastName { get; set; }

		//Key = Email
		public string Email { get; set; }

	}
}