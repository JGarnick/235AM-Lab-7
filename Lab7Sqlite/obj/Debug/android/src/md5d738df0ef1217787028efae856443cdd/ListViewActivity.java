package md5d738df0ef1217787028efae856443cdd;


public class ListViewActivity
	extends android.app.ListActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Lab7Sqlite.ListViewActivity, Lab7Sqlite, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ListViewActivity.class, __md_methods);
	}


	public ListViewActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ListViewActivity.class)
			mono.android.TypeManager.Activate ("Lab7Sqlite.ListViewActivity, Lab7Sqlite, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
