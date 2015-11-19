using CorflowMobile.Data;
using CorflowMobile.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Diagnostics;

namespace CorflowMobile.Views
{
	public class IconLabelView : ContentView
	{
        private List<Adres> adressen;
        private List<Gemeente> gemeentes;
        string adrestText;
		private TimeSpan einde,start;
		private List<Prestatie> presatielijst;
		private Image img;

		public IconLabelView (Opdracht opdracht,FileImageSource source,String text)
		{
            adressen = (List<Adres>)DependencyService.Get<IDataService>().LoadAll<Adres>();
			presatielijst = (List<Prestatie>)DependencyService.Get<IDataService>().LoadAll<Prestatie>();
            gemeentes = (List<Gemeente>)DependencyService.Get<IDataService>().LoadAll<Gemeente>();
            adrestText = adressen[opdracht.Adres].Straat + " " + adressen[opdracht.Adres].Nummer + ", " + gemeentes[adressen[opdracht.Adres].Gemeente].Postcode + " " + gemeentes[adressen[opdracht.Adres].Gemeente].Plaats;
            BackgroundColor = StyleKit.CardFooterBackgroundColor;

			img = new Image () { 
				Source = source, 
				HeightRequest = 10, 
				WidthRequest = 10 
			};
	

			var label = new Label () {
				Text = text,
				FontSize = 9,
				FontAttributes = FontAttributes.Bold,
				TextColor = StyleKit.LightTextColor
			};

			var tapGestureRecognizer = new TapGestureRecognizer ();
			tapGestureRecognizer.Tapped += (sender, e) => {

				if (label.Text == "Navigeer") {
					label.Text = "geklikt";
					Geocode geo = new Geocode ();
					geo.Latitude = opdracht.Latitude;
					geo.Longitude = opdracht.Longitude;

					Place place = new Place ();

					if (Device.OS == TargetPlatform.iOS) {
						place.Name = adrestText;
						place.Vicinity = "Brugge";
					} else {
						place.Name = "Brugge";
						place.Vicinity = adrestText;
					}
					place.Location = geo;

					LaunchMapApp (place);

				} else if (label.Text == "Start") {
					label.Text = "Stop";
					opdracht.Statuslabel = "Stop";
					img.Source = StyleKit.Icons.Stop;
					opdracht.starttijd = DateTime.Now;
					DependencyService.Get<IDataService>().Update(opdracht);

					bool found = false;
					foreach(Prestatie pres in presatielijst)
					{
						if(pres.OpdrachtID == opdracht.ID){
							found = true;
						}
					}

					if (!found) {
						Prestatie presatie = new Prestatie();
						presatie.Aanvang = DateTime.Now;
						presatie.Duur = 0;
						presatie.OpdrachtID = opdracht.ID;
						DependencyService.Get<IDataService>().Insert(presatie);
						SyncController.Instance.TrySync();
					}


				}else if (label.Text == "Stop") {
					label.Text = "Start";
					opdracht.Statuslabel = "Start";
					img.Source = StyleKit.Icons.Resume;
					Debug.WriteLine ("gevonden tijd: " + opdracht.starttijd);
					start = opdracht.starttijd.Value.TimeOfDay;
					DependencyService.Get<IDataService>().Update(opdracht);
					einde = DateTime.Now.TimeOfDay;
					TimeSpan tijd = CalculateTime();

					foreach(Prestatie pres in presatielijst)
					{
						if(pres.OpdrachtID == opdracht.ID){
							pres.Duur = pres.Duur + Convert.ToDecimal(tijd.TotalMinutes) ;
							DependencyService.Get<IDataService>().Update(pres);
							SyncController.Instance.TrySync();
						}
					}
				}
			};

			label.GestureRecognizers.Add(tapGestureRecognizer);



            //TO-DO: latitude & longitude in een navigeerknop.
            //opdracht.Latitude, opdracht.Longitude

			var stack = new StackLayout () {
				Padding = new Thickness (5),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.Center,
				Children = {
					img,label
				}
			};

			Content = stack;
		}

		private TimeSpan CalculateTime(){
			TimeSpan tijd = new TimeSpan();
			tijd = einde - start ;
			Debug.WriteLine ("gevonden tijd: " + tijd);
			return tijd;
		}

		public void LaunchMapApp(Place place) {
			// Windows Phone doesn't like ampersands in the names and the normal URI escaping doesn't help
			var name = place.Name.Replace("&", "and"); // var name = Uri.EscapeUriString(place.Name);
			var loc = string.Format("{0},{1}", place.Location.Latitude, place.Location.Longitude);
			var addr = Uri.EscapeUriString(place.Vicinity);

			var request = Device.OnPlatform(
				// iOS doesn't like %s or spaces in their URLs, so manually replace spaces with +s
				string.Format("http://maps.apple.com/maps?q={0}&sll={1}", name.Replace(' ', '+'), loc),

				// pass the address to Android if we have it
				string.Format("geo:0,0?q={0}({1})", string.IsNullOrWhiteSpace(addr) ? loc : addr, name),

				// WinPhone
				string.Format("bingmaps:?cp={0}&q={1}", loc, name)
			);

			Device.OpenUri(new Uri(request));
		}
	}
}