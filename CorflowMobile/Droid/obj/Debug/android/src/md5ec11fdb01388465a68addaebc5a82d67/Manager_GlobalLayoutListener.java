package md5ec11fdb01388465a68addaebc5a82d67;


public class Manager_GlobalLayoutListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.ViewTreeObserver.OnGlobalLayoutListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onGlobalLayout:()V:GetOnGlobalLayoutHandler:Android.Views.ViewTreeObserver/IOnGlobalLayoutListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Toasts.Forms.Plugin.Droid.Manager+GlobalLayoutListener, Toasts.Forms.Plugin.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Manager_GlobalLayoutListener.class, __md_methods);
	}


	public Manager_GlobalLayoutListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Manager_GlobalLayoutListener.class)
			mono.android.TypeManager.Activate ("Toasts.Forms.Plugin.Droid.Manager+GlobalLayoutListener, Toasts.Forms.Plugin.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onGlobalLayout ()
	{
		n_onGlobalLayout ();
	}

	private native void n_onGlobalLayout ();

	java.util.ArrayList refList;
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
