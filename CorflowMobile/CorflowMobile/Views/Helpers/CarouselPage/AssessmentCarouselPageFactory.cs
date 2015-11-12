using CorflowMobile.Controllers;
using System;
using Xamarin.Forms;

namespace CorflowMobile.Views
{
    /* DEZE KLASSE WORDT MOMENTEEL NIET GEBRUIKT !!! */

    class AssessmentCarouselPageFactory
    {
        private static AssessmentCarouselPageFactory instance;

        public static AssessmentCarouselPageFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new AssessmentCarouselPageFactory();

                return instance;
            }
        }

        public CarouselPage CreateAssessmentCarouselPage(DateTime? date = null)
        {
            DateTime dateNotNull = date == null ? DateTime.Today : (DateTime)date;
            CarouselPage assessmentCarouselPage = new CarouselPage() {
                Children = {
                        new AssessmentPage(dateNotNull.AddDays(-1)),
                        new AssessmentPage(dateNotNull),
                        new AssessmentPage(dateNotNull.AddDays(1))
                    },
                Title = String.Format("{0} voor {1}",
                    LoginController.Instance.GetCurrentUser.Functie == 2 ? "Afspraken" : "Werkopdrachten",
                    dateNotNull == DateTime.Today ? "vandaag" : date.ToString())
        };
            assessmentCarouselPage.CurrentPage = assessmentCarouselPage.Children[1];
            return assessmentCarouselPage;
        }
    }
}
