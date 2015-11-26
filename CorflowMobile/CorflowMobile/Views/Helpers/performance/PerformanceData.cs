using System;

using CorflowMobile.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using CorflowMobile.Data;
using System.Linq;
using CorflowMobile.Controllers;

namespace CorflowMobile.Views
{
    public class PerformanceData
    {
        public static List<Performance> GetData(int prestatieID)
        {
            List<PrestatiesPrestatiesoorten> ListPrestatiesPrestatiesoort = DataController.Instance.GetAchievementsAchievementTypeByAchievementID(prestatieID);
            List<Prestatiesoort> ListPrestatieSoort = DataController.Instance.GetAchievementtypes();
            List<Performance> ListPerformance = new List<Performance>();


            if (ListPrestatiesPrestatiesoort.Count > 0)
            {
                foreach (PrestatiesPrestatiesoorten pres in ListPrestatiesPrestatiesoort)
                {
                    foreach (Prestatiesoort soort in ListPrestatieSoort)
                    {
                        if (pres.PrestatieSoortID == soort.ID)
                        {
                            Performance per = new Performance
                            {
                                Name = soort.Omschrijving
                            };
                            ListPerformance.Add(per);
                        }
                    }
                }
            }
            else
            {
                ListPerformance.Add(new Performance
                {
                    Name = "Nog Geen Perfomance"
                });
            }

            return ListPerformance;

        }
    }
}
