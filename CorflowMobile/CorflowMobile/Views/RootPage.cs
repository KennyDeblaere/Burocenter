using System;

using Xamarin.Forms;

namespace CorflowMobile.Views
{
	public class RootPage : MasterDetailPage
	{
		MenuPage menuPage;

		public RootPage ()
		{
			menuPage = new MenuPage ();

			menuPage.Menu.ItemSelected += (sender, e) => NavigateTo (e.SelectedItem as MenuItem);

			Master = menuPage;
			Detail = new NavigationPage (new AssessmentPage(DateTime.Today));
		}

		public void NavigateTo (MenuItem menu, DateTime? date = null)
		{
			if (menu == null)
				return;

            Page displayPage;

            if (menu.TargetType == typeof(AssessmentPage))
            {
                displayPage = (Page)Activator.CreateInstance(menu.TargetType, date == null ? DateTime.Today : date);
            }
            else
            {
                displayPage = (Page)Activator.CreateInstance(menu.TargetType);
            }

			Detail = new NavigationPage (displayPage);

			menuPage.Menu.SelectedItem = null;
			IsPresented = false;
		}
	}
}


