package md5e038db2f670d5c4a496bf3606cfc5e72;


public class ImageCircleRenderer
	extends md5282f1122c1313907b9bf274dd2c2f344.ImageRenderer
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_drawChild:(Landroid/graphics/Canvas;Landroid/view/View;J)Z:GetDrawChild_Landroid_graphics_Canvas_Landroid_view_View_JHandler\n" +
			"";
		mono.android.Runtime.register ("ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer, ImageCircle.Forms.Plugin.Android, Version=1.1.6.0, Culture=neutral, PublicKeyToken=null", ImageCircleRenderer.class, __md_methods);
	}


	public ImageCircleRenderer (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == ImageCircleRenderer.class)
			mono.android.TypeManager.Activate ("ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer, ImageCircle.Forms.Plugin.Android, Version=1.1.6.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public ImageCircleRenderer (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == ImageCircleRenderer.class)
			mono.android.TypeManager.Activate ("ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer, ImageCircle.Forms.Plugin.Android, Version=1.1.6.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public ImageCircleRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == ImageCircleRenderer.class)
			mono.android.TypeManager.Activate ("ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer, ImageCircle.Forms.Plugin.Android, Version=1.1.6.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public boolean drawChild (android.graphics.Canvas p0, android.view.View p1, long p2)
	{
		return n_drawChild (p0, p1, p2);
	}

	private native boolean n_drawChild (android.graphics.Canvas p0, android.view.View p1, long p2);

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
