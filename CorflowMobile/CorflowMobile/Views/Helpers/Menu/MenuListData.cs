using Xamarin.Forms;
using System.Collections.Generic;
using CorflowMobile.Controllers;

namespace CorflowMobile.Views
{
	public class MenuListData : List<MenuItem>
	{
		public MenuListData ()
		{
			this.Add (new MenuItem () { 
				Title = string.Format("{0} voor vandaag", LoginController.Instance.GetCurrentUser.Functie == 2 ? "Afspraken" : "Werkopdrachten"), 
				IconSource = "checking1.png", 
				TargetType = typeof(AssessmentPage)
			});

            this.Add(new MenuItem()
            {
                Title = "Zoek contacten",
                IconSource = "contacts13.png",
                TargetType = typeof(ContactsPage)
            });

            if (LoginController.Instance.GetCurrentUser.Functie == 2)
            {
                this.Add(new MenuItem()
                {
                    Title = "Zoek afspraken",
                    IconSource = "history2.png",
                    TargetType = typeof(CalendarPage)
                });
            }
        }
	}
}

