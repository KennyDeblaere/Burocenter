using System;
using System.Text;
using SQLite;

namespace CorflowMobile.Models
{

	/**
	 * This class presents a simple ORM over the Zumero-synced SQLite 
	 * table.  Some properties may require conversion from C# objects
	 * to the representation that Zumero requires in SQLite.  For more
	 * information on why some data types are converted when stored in 
	 * a Zumero-synced SQLite database, see: 
	 * http://zumero.com/docs/zumero_for_sql_server_manager.html#data_type_conversion_and_limitations
	 */
	[Table("Opdracht")]
	public class Opdracht : BaseModel
	{
		public Opdracht()
		{
			//Don't fire notfications by default, since
			//they make editing the properties difficult.
			this.NotifyIfPropertiesChange = false;
		}



		

		[PrimaryKey]
        [NotNull]
		[AutoIncrement, Column("ID")]
		public int ID 
		{ 
			get { return ID_private; }
			set { SetProperty(ID_private, value, (val) => { ID_private = val; }, ID_PropertyName); }
		}
		public static string ID_PropertyName = "ID";
		private int ID_private;
		
		


        [NotNull]
		[Column("Handtekening")]
		public bool Handtekening 
		{ 
			get { return Handtekening_private; }
			set { SetProperty(Handtekening_private, value, (val) => { Handtekening_private = val; }, Handtekening_PropertyName); }
		}
		public static string Handtekening_PropertyName = "Handtekening";
		private bool Handtekening_private;
		
		



		[Column("Afgewerkt")]
		public Nullable<bool> Afgewerkt 
		{ 
			get { return Afgewerkt_private; }
			set { SetProperty(Afgewerkt_private, value, (val) => { Afgewerkt_private = val; }, Afgewerkt_PropertyName); }
		}
		public static string Afgewerkt_PropertyName = "Afgewerkt";
		private Nullable<bool> Afgewerkt_private;
		
		


        [NotNull]
		[Column("BedrijfID")]
		public int BedrijfID 
		{ 
			get { return BedrijfID_private; }
			set { SetProperty(BedrijfID_private, value, (val) => { BedrijfID_private = val; }, BedrijfID_PropertyName); }
		}
		public static string BedrijfID_PropertyName = "BedrijfID";
		private int BedrijfID_private;
		
		



		[Column("Omschrijving")]
		public string Omschrijving 
		{ 
			get { return Omschrijving_private; }
			set { SetProperty(Omschrijving_private, value, (val) => { Omschrijving_private = val; }, Omschrijving_PropertyName); }
		}
		public static string Omschrijving_PropertyName = "Omschrijving";
		private string Omschrijving_private;
		
		



		[Column("Latitude")]
		public string Latitude 
		{ 
			get { return Latitude_private; }
			set { SetProperty(Latitude_private, value, (val) => { Latitude_private = val; }, Latitude_PropertyName); }
		}
		public static string Latitude_PropertyName = "Latitude";
		private string Latitude_private;
		
		



		[Column("Longitude")]
		public string Longitude 
		{ 
			get { return Longitude_private; }
			set { SetProperty(Longitude_private, value, (val) => { Longitude_private = val; }, Longitude_PropertyName); }
		}
		public static string Longitude_PropertyName = "Longitude";
		private string Longitude_private;
		
		


        [NotNull]
		[Column("Adres")]
		public int Adres 
		{ 
			get { return Adres_private; }
			set { SetProperty(Adres_private, value, (val) => { Adres_private = val; }, Adres_PropertyName); }
		}
		public static string Adres_PropertyName = "Adres";
		private int Adres_private;
		
		



		[Column("VerstuurdeEmail")]
		public Nullable<bool> VerstuurdeEmail 
		{ 
			get { return VerstuurdeEmail_private; }
			set { SetProperty(VerstuurdeEmail_private, value, (val) => { VerstuurdeEmail_private = val; }, VerstuurdeEmail_PropertyName); }
		}
		public static string VerstuurdeEmail_PropertyName = "VerstuurdeEmail";
		private Nullable<bool> VerstuurdeEmail_private;

		[Column("statuslabel")]
		public string Statuslabel 
		{ 
			get { return Statuslabel_private; }
			set { SetProperty(Statuslabel_private, value, (val) => { Statuslabel_private = val; },Statuslabel_PropertyName); }
		}
		public static string Statuslabel_PropertyName = "Statuslabel";
		private string Statuslabel_private;


		[Column("Starttijd")]

		// The actual column definition, as seen in SQLite
		public string starttijd_raw { get; set; }

		public static string Starttijd_PropertyName = "starttijd";

		// A helper definition that will not be saved to SQLite directly.
		// This property reads and writes to the _raw property.
		[Ignore]
		public Nullable<DateTime> starttijd { 
			// Watch out for time zones, as they are not encoded into
			// the database. Here, I make no assumptions about time
			// zones.
			get { return starttijd_raw != null ? (Nullable<DateTime>)DateTime.Parse(starttijd_raw) : (Nullable<DateTime>)null; }
			set { SetProperty(starttijd_raw, starttijd_ConvertToString(value), (val) => { starttijd_raw = val; },Starttijd_PropertyName); }
		}

		// This static method is helpful when you need to query
		// on the raw value.
		public static string starttijd_ConvertToString(Nullable<DateTime> date)
		{
			if (!date.HasValue)
				return null;
			else

				return date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");

		}

		
		


		public override string ToString() 
		{
			StringBuilder sb = new StringBuilder();


			sb.Append(ID.ToString());

			sb.Append("|");

			sb.Append(Handtekening.ToString());

			sb.Append("|");

			if (Afgewerkt.HasValue)
			{
				sb.Append(Afgewerkt.ToString());
			}
			sb.Append("|");

			sb.Append(BedrijfID.ToString());

			sb.Append("|");

			if (Omschrijving != null)
			{
				sb.Append(Omschrijving.ToString());
			}
			sb.Append("|");

			if (Latitude != null)
			{
				sb.Append(Latitude.ToString());
			}
			sb.Append("|");

			if (Longitude != null)
			{
				sb.Append(Longitude.ToString());
			}
			sb.Append("|");

			sb.Append(Adres.ToString());

			sb.Append("|");

			if (VerstuurdeEmail.HasValue)
			{
				sb.Append(VerstuurdeEmail.ToString());
			}
			sb.Append("|");

			return sb.ToString();
		}
	}
}
