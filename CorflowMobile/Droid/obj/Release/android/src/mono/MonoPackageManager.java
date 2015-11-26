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

	public static void LoadApplication (Context context, ApplicationInfo runtimePackage, String[] apks)
	{
		synchronized (lock) {
			if (!initialized) {
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
				initialized = true;
			}
		}
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
		"CorflowMobile.Droid.dll",
		"SQLite-net.dll",
		"Connectivity.Plugin.dll",
		"Connectivity.Plugin.Abstractions.dll",
		"Microsoft.IdentityModel.Clients.ActiveDirectory.dll",
		"Microsoft.IdentityModel.Clients.ActiveDirectory.Platform.dll",
		"ZXingNetMobile.dll",
		"zxing.portable.dll",
		"Zumero.dll",
		"Xamarin.Android.Support.v7.AppCompat.dll",
		"Xamarin.Android.Support.Design.dll",
		"Xamarin.Android.Support.v7.CardView.dll",
		"Xamarin.Android.Support.v7.MediaRouter.dll",
		"Xamarin.Android.Support.v4.dll",
		"Xamarin.GooglePlayServices.Base.dll",
		"Xamarin.GooglePlayServices.Maps.dll",
		"FormsViewGroup.dll",
		"ImageCircle.Forms.Plugin.Abstractions.dll",
		"ImageCircle.Forms.Plugin.Android.dll",
		"Toasts.Forms.Plugin.Abstractions.dll",
		"Toasts.Forms.Plugin.Droid.dll",
		"Xamarin.Forms.Core.dll",
		"Xamarin.Forms.Maps.dll",
		"Xamarin.Forms.Maps.Android.dll",
		"Xamarin.Forms.Platform.dll",
		"Xamarin.Forms.Platform.Android.dll",
		"Xamarin.Forms.Xaml.dll",
		"XLabs.Ioc.dll",
		"ExifLib.dll",
		"XLabs.Core.dll",
		"XLabs.Platform.dll",
		"XLabs.Platform.Droid.dll",
		"XLabs.Serialization.dll",
		"XLabs.Forms.dll",
		"XLabs.Forms.Droid.dll",
		"SQLitePCL.raw.dll",
		"CorflowMobile.dll",
		"System.Reflection.Emit.ILGeneration.dll",
		"System.Reflection.Emit.Lightweight.dll",
		"System.Reflection.Emit.dll",
	};
	public static final String[] Dependencies = new String[]{
	};
	public static final String ApiPackageName = null;
}
