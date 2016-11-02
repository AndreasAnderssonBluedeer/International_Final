using System;
using System.Threading.Tasks;

namespace CatchUp.Core.Interfaces
{
	public interface IOptionsDatabase
	{

		//??????
		Task<bool> Vibration();
		Task<bool> Sound();
		Task<bool> DisturbMode();
		Task<string> AutoMessage();
		Task<string> FirstName();
		Task<string> LastName();

	}
}

