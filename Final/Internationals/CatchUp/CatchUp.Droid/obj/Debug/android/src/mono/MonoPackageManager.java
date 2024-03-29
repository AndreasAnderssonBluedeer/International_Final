package mono;

import java.io.*;
import java.lang.String;
import java.util.Locale;
import java.util.HashSet;
import java.util.zip.*;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.res.AssetManager;
import android.util.Log;
import mono.android.Runtime;

public class MonoPackageManager {

	static Object lock = new Object ();
	static boolean initialized;

	static android.content.Context Context;

	public static void LoadApplication (Context context, ApplicationInfo runtimePackage, String[] apks)
	{
		synchronized (lock) {
			if (context instanceof android.app.Application) {
				Context = context;
			}
			if (!initialized) {
				android.content.IntentFilter timezoneChangedFilter  = new android.content.IntentFilter (
						android.content.Intent.ACTION_TIMEZONE_CHANGED
				);
				context.registerReceiver (new mono.android.app.NotifyTimeZoneChanges (), timezoneChangedFilter);
				
				System.loadLibrary("monodroid");
				Locale locale       = Locale.getDefault ();
				String language     = locale.getLanguage () + "-" + locale.getCountry ();
				String filesDir     = context.getFilesDir ().getAbsolutePath ();
				String cacheDir     = context.getCacheDir ().getAbsolutePath ();
				String dataDir      = getNativeLibraryPath (context);
				ClassLoader loader  = context.getClassLoader ();

				Runtime.init (
						language,
						apks,
						getNativeLibraryPath (runtimePackage),
						new String[]{
							filesDir,
							cacheDir,
							dataDir,
						},
						loader,
						new java.io.File (
							android.os.Environment.getExternalStorageDirectory (),
							"Android/data/" + context.getPackageName () + "/files/.__override__").getAbsolutePath (),
						MonoPackageManager_Resources.Assemblies,
						context.getPackageName ());
				
				mono.android.app.ApplicationRegistration.registerApplications ();
				
				initialized = true;
			}
		}
	}

	public static void setContext (Context context)
	{
		// Ignore; vestigial
	}

	static String getNativeLibraryPath (Context context)
	{
	    return getNativeLibraryPath (context.getApplicationInfo ());
	}

	static String getNativeLibraryPath (ApplicationInfo ainfo)
	{
		if (android.os.Build.VERSION.SDK_INT >= 9)
			return ainfo.nativeLibraryDir;
		return ainfo.dataDir + "/lib";
	}

	public static String[] getAssemblies ()
	{
		return MonoPackageManager_Resources.Assemblies;
	}

	public static String[] getDependencies ()
	{
		return MonoPackageManager_Resources.Dependencies;
	}

	public static String getApiPackageName ()
	{
		return MonoPackageManager_Resources.ApiPackageName;
	}
}

class MonoPackageManager_Resources {
	public static final String[] Assemblies = new String[]{
		/* We need to ensure that "CatchUp.Droid.dll" comes first in this list. */
		"CatchUp.Droid.dll",
		"CatchUp.Core.dll",
		"Java.Interop.dll",
		"Microsoft.WindowsAzure.Mobile.dll",
		"Microsoft.WindowsAzure.Mobile.Ext.dll",
		"Microsoft.WindowsAzure.Mobile.SQLiteStore.dll",
		"MvvmCross.Binding.dll",
		"MvvmCross.Binding.Droid.dll",
		"MvvmCross.Core.dll",
		"MvvmCross.Droid.dll",
		"MvvmCross.Droid.Shared.dll",
		"MvvmCross.FieldBinding.dll",
		"MvvmCross.Localization.dll",
		"MvvmCross.Platform.dll",
		"MvvmCross.Platform.Droid.dll",
		"MvvmCross.Plugins.FieldBinding.dll",
		"MvvmCross.Plugins.Messenger.dll",
		"MvvmCross.Plugins.Validation.dll",
		"MvvmCross.Plugins.Validation.Droid.dll",
		"MvvmCross.Plugins.Validation.ForFieldBinding.dll",
		"Newtonsoft.Json.dll",
		"SQLite.Net.dll",
		"SQLite.Net.Platform.XamarinAndroid.dll",
		"SQLitePCLRaw.batteries_green.dll",
		"SQLitePCLRaw.core.dll",
		"SQLitePCLRaw.lib.e_sqlite3.dll",
		"SQLitePCLRaw.provider.e_sqlite3.dll",
		"System.Net.Http.Extensions.dll",
		"System.Net.Http.Formatting.dll",
		"System.Net.Http.Primitives.dll",
		"Xamarin.Android.Support.v4.dll",
		"Xamarin.GooglePlayServices.Base.dll",
		"Xamarin.GooglePlayServices.Basement.dll",
		"Xamarin.GooglePlayServices.Maps.dll",
		"Owin.dll",
		"System.Web.Http.dll",
		"System.Web.Http.Tracing.dll",
		"System.Runtime.dll",
		"System.Resources.ResourceManager.dll",
		"System.Diagnostics.Debug.dll",
		"System.Threading.Tasks.dll",
		"System.Collections.dll",
		"System.Linq.Expressions.dll",
		"System.ObjectModel.dll",
		"System.Linq.dll",
		"System.Reflection.dll",
		"System.Threading.dll",
		"System.Collections.Concurrent.dll",
		"System.Runtime.InteropServices.dll",
		"System.Runtime.Extensions.dll",
		"System.Reflection.Extensions.dll",
		"System.ServiceModel.Internals.dll",
		"System.Text.RegularExpressions.dll",
		"System.Net.Primitives.dll",
		"System.Diagnostics.Tools.dll",
		"System.Globalization.dll",
		"System.Runtime.Serialization.Primitives.dll",
		"System.Text.Encoding.dll",
		"System.Linq.Queryable.dll",
		"System.IO.dll",
		"System.Xml.XDocument.dll",
		"System.Dynamic.Runtime.dll",
		"System.Xml.ReaderWriter.dll",
		"System.Text.Encoding.Extensions.dll",
		"System.ComponentModel.EventBasedAsync.dll",
		"System.Runtime.Serialization.Xml.dll",
		"System.Xml.XmlSerializer.dll",
	};
	public static final String[] Dependencies = new String[]{
	};
	public static final String ApiPackageName = "Mono.Android.Platform.ApiLevel_23";
}
