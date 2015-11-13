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
        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarPage"/> class.
        /// </summary>
        public CalendarPage()
        {
            CalendarView calendarView = new CalendarView()
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

            calendarView.DateSelected += (object sender, DateTime e) =>
            {
				Navigation.PushAsync(new ListCalendarPage(e));
            };

            Content = calendarView;
        }
    }
}
