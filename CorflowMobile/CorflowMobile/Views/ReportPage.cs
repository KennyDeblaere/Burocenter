using System;
using CorflowMobile.Models;

using Xamarin.Forms;
using CorflowMobile.Data;
using CorflowMobile.Superclasses;

namespace CorflowMobile.Views
{
    public class ReportPage : ContentPage
	{
		private StackLayout layout,titelstack;
		private Editor edReport;
		private Button addReport;
        private Label lblTitle, lblReport;
        private Entry txtTitle;

        private SyncItems syncItems;

        
		public ReportPage (Prestatie achievement)
		{
			Title="Verslag";
			Padding = new Thickness (10, 10, 10, 10);

            syncItems = new SyncItems();

            InitComponents(achievement);
            InitLabels();
            InitButton(achievement);

			layout.Children.Add (titelstack);
			layout.Children.Add (lblReport);
			layout.Children.Add (edReport);
			layout.Children.Add (addReport);

            Navigation.PopAsync();

        }
		protected override void OnAppearing ()
		{
			Content = layout;
			base.OnAppearing ();
		}

		protected override void OnDisappearing ()
		{
			Content = null;
			base.OnDisappearing ();
		}

        private void InitComponents(Prestatie achievement)
        {
            txtTitle = new Entry
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            edReport = new Editor
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            if (syncItems.GetReportsByAchievement(achievement).Count > 0)
            {
                txtTitle.Text = syncItems.GetReportsByAchievement(achievement)[0].Titel;
                edReport.Text = syncItems.GetReportsByAchievement(achievement)[0].verslag;
            }
            else
            {
                txtTitle.Placeholder = "Heef titel in";
            }

            layout = new StackLayout();

            titelstack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };
        }
        private void InitLabels()
        {
            lblTitle = new Label
            {
                Text = "Titel: "
            };

            titelstack.Children.Add(lblTitle);
            titelstack.Children.Add(txtTitle);


            lblReport = new Label
            {
                Text = "Verslag:"
            };
        }
        private void InitButton(Prestatie achievement)
        {
            addReport = new Button
            {
                Text = "Toevoegen verslag"
            };
            addReport.Clicked += (object sender, EventArgs e) => {


                if (syncItems.GetReportsByAchievement(achievement).Count > 0)
                {
                    syncItems.GetReportsByAchievement(achievement)[0].Titel = txtTitle.Text;
                    syncItems.GetReportsByAchievement(achievement)[0].verslag = edReport.Text;
                    DependencyService.Get<IDataService>().Update(syncItems.GetReportsByAchievement(achievement)[0]);
                    SyncController.Instance.TrySync();

                }
                else
                {
                    Verslag report = new Verslag();
                    report.Titel = txtTitle.Text;
                    report.verslag = edReport.Text;
                    report.Prestatie = achievement.ID;
                    DependencyService.Get<IDataService>().Insert(report);
                    SyncController.Instance.TrySync();
                }
            };
        }
    }
}


