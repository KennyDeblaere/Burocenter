using System;
using CorflowMobile.Models;

using Xamarin.Forms;
using CorflowMobile.Data;
using System.Collections.Generic;
using CorflowMobile.Controllers;

namespace CorflowMobile.Views
{
    public class ReportPage : ContentPage
	{
		private StackLayout layout,titelstack;
		private Editor edReport;
		private Button addReport;
        private Label lblTitle, lblReport;
        private Entry txtTitle;

        
		public ReportPage (Prestatie achievement)
		{
			Title="Verslag";
			Padding = new Thickness (10, 10, 10, 10);

            InitComponents(achievement);
            InitLabels();
            InitButton(achievement);

			layout.Children.Add (titelstack);
			layout.Children.Add (lblReport);
			layout.Children.Add (edReport);
			layout.Children.Add (addReport);

            

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

            if (DataController.Instance.GetReportsByAchievement(achievement).Count > 0)
            {
                txtTitle.Text = DataController.Instance.GetReportsByAchievement(achievement)[0].Titel;
                edReport.Text = DataController.Instance.GetReportsByAchievement(achievement)[0].verslag;
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


                if (DataController.Instance.GetReportsByAchievement(achievement).Count > 0)
                {
					List<Verslag> report = DataController.Instance.GetReportsByAchievement(achievement);
					report[0].Titel = txtTitle.Text;
					report[0].verslag = edReport.Text;
					DataController.Instance.Update(report[0]);
                    SyncController.Instance.SyncNeeded();

                }
                else
                {
                    Verslag report = new Verslag();
                    report.Titel = txtTitle.Text;
                    report.verslag = edReport.Text;
                    report.Prestatie = achievement.ID;
                    DataController.Instance.Insert(report);
                    SyncController.Instance.SyncNeeded();
                }

				Navigation.PopAsync();
            };
        }
    }
}


