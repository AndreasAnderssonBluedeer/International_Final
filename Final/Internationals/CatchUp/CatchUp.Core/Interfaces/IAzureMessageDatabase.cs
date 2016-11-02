using System;
using Microsoft.WindowsAzure.MobileServices;

namespace CatchUp.Core
{
	public interface IAzureMessageDatabase
	{
		MobileServiceClient GetMobileServiceClient();
	}
}

