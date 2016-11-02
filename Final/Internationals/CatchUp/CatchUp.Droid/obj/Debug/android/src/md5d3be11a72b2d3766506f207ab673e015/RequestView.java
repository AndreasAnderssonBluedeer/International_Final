package md5d3be11a72b2d3766506f207ab673e015;


public class RequestView
	extends mvvmcross.droid.views.MvxActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("CatchUp.Droid.Views.RequestView, CatchUp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", RequestView.class, __md_methods);
	}


	public RequestView () throws java.lang.Throwable
	{
		super ();
		if (getClass () == RequestView.class)
			mono.android.TypeManager.Activate ("CatchUp.Droid.Views.RequestView, CatchUp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
