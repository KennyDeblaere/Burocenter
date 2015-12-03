using System;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CorflowMobile.Views
{
    /// <summary>
    /// Class CalendarPage.
    /// </summary>
    public partial class CalendarPage : ContentPage
    {
		CalendarView caldendarView;
        public CalendarPage()
        {
			caldendarView = new CalendarView()
            {
                //BackgroundColor = Color.Blue,
                MinDate = CalendarView.FirstDayOfMonth(DateTime.Now.AddYears(-1)),
                MaxDate = CalendarView.LastDayOfMonth(DateTime.Now.AddYears(1)),
                HighlightedDateBackgroundColor = Color.FromRgb(227, 227, 227),
                ShouldHighlightDaysOfWeekLabels = false,
                SelectionBackgroundStyle = CalendarView.BackgroundStyle.CircleFill,
                TodayBackgroundStyle = CalendarView.BackgroundStyle.CircleOutline,
                HighlightedDaysOfWeek = new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday },
                ShowNavigationArrows = true,
                MonthTitleFont = Font.OfSize("Open 24 Display St", NamedSize.Medium),
            };

			caldendarView.DateSelected += (object sender, DateTime e) =>
            {
				Navigation.PushAsync(new ListCalendarPage(e));
            };

        }

		protected override void OnAppearing()
		{
			Content = caldendarView;
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			Content = null;
			base.OnDisappearing();
		}
    }
}
