using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CorflowMobile.Views.SplashPage), typeof(CorflowMobile.Droid.CustomRenderer))]
[assembly: ExportRenderer(typeof(CorflowMobile.Views.LoginPage), typeof(CorflowMobile.Droid.CustomRenderer))]
[assembly: ExportRenderer(typeof(CorflowMobile.Views.MenuPage), typeof(CorflowMobile.Droid.CustomRenderer))]
[assembly: ExportRenderer(typeof(CorflowMobile.Views.ContactsPage), typeof(CorflowMobile.Droid.CustomRenderer))]
[assembly: ExportRenderer(typeof(CorflowMobile.Views.AssessmentPage), typeof(CorflowMobile.Droid.CustomRenderer))]
[assembly: ExportRenderer(typeof(CorflowMobile.Views.AssessmentDetailPage), typeof(CorflowMobile.Droid.CustomRenderer))]
namespace CorflowMobile.Droid
{
    //This customer renderer exists only to make certain that we cache the activity
    //hosting the syncPage.  That activity is then used to start the background
    //service.
    public class CustomRenderer : PageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
		{
			AndroidSyncService.SyncActivity = this.Context as Activity;
            base.OnElementChanged(e);
        }

        protected override void OnAttachedToWindow()
		{
			AndroidSyncService.SyncActivity = this.Context as Activity;
            base.OnAttachedToWindow();
        }

        protected override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();
            AndroidSyncService.SyncActivity = null;
        }
    }
}