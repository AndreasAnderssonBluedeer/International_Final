using System;
using Microsoft.WindowsAzure.MobileServices;

namespace CatchUp.Core.Interfaces
{
	public interface IAzureChatDatabase
	{
		MobileServiceClient GetMobileServiceClient();
	}
}

