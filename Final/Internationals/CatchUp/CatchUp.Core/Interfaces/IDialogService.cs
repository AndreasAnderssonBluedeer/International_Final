using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatchUp.Core.Interfaces
{
	public interface IDialogService
	{
		Task<int> Show(string message, string title);
		Task<int> Show(string message, string title, string confirmButton);
		Task<int> Show(string message, string title, string confirmButton, string cancelButton);
	}
}
