using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform;
using CatchUp.Core.Interfaces;
using CatchUp.Droid.Database;
using CatchUp.Core.Database;
using CatchUp.Core;
using MvvmCross.Plugins.Validation;
using MvvmCross.Plugins.Validation.Droid;
using CatchUp.Droid.Services;
using MvvmCrossDemo.Droid.Maps;

namespace CatchUp.Droid
{
	public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

		protected override void InitializePlatformServices()
		{
			base.InitializePlatformServices();

			Mvx.RegisterType<IMvxToastService>(() => new MvxAndroidToastService(ApplicationContext));
		}
        protected override IMvxApplication CreateApp()
        {
            return new CatchUp.Core.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

		protected override void InitializeFirstChance()
		{
			Mvx.LazyConstructAndRegisterSingleton<ISqlite, SqliteDroid>();
			Mvx.LazyConstructAndRegisterSingleton<IDialogService, DialogService>();
			Mvx.LazyConstructAndRegisterSingleton<IAzureDatabase, AzureDatabase>();
			Mvx.LazyConstructAndRegisterSingleton<IAzureMessageDatabase, AzureMessageDatabase>();
			Mvx.LazyConstructAndRegisterSingleton<IAzureChatDatabase, AzureChatDatabase>();
			Mvx.LazyConstructAndRegisterSingleton<IAzureContactRequestDatabase, AzureContactRequestDatabase>();

			//Google Maps
			Mvx.LazyConstructAndRegisterSingleton<IGeoCoder, GeoCoder>();

			Mvx.LazyConstructAndRegisterSingleton<IUserStorageDatabase, UserStorageDatabase>();
			Mvx.LazyConstructAndRegisterSingleton<ILocalContactDatabase, LocalContactDatabase>();
			//uncomment the below if you only want to use local storage
			Mvx.LazyConstructAndRegisterSingleton<IUserDatabase, UserDatabaseAzure>();
			Mvx.LazyConstructAndRegisterSingleton<IContactRequestDatabase, ContactRequestDatabaseAzure>();
			Mvx.LazyConstructAndRegisterSingleton<IChatDatabase, ChatDatabaseAzure>();
			Mvx.LazyConstructAndRegisterSingleton<IMessageDatabase, MessageDatabaseAzure>();
			base.InitializeFirstChance();
		}

	}
}
