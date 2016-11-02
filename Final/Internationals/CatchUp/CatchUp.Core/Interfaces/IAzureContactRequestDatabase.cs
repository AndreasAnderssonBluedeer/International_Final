using System;
using Microsoft.WindowsAzure.MobileServices;

namespace CatchUp.Core
{
	public interface IAzureContactRequestDatabase
	{
		MobileServiceClient GetMobileServiceClient();
	}
}

