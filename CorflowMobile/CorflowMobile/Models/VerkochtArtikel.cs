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
	[Table("VerkochtArtikel")]
	public class VerkochtArtikel : BaseModel
	{
		public VerkochtArtikel()
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
		[Column("Artikel")]
		public int Artikel 
		{ 
			get { return Artikel_private; }
			set { SetProperty(Artikel_private, value, (val) => { Artikel_private = val; }, Artikel_PropertyName); }
		}
		public static string Artikel_PropertyName = "Artikel";
		private int Artikel_private;
		
		


        [NotNull]
		[Column("Serienummer")]
		public string Serienummer 
		{ 
			get { return Serienummer_private; }
			set { SetProperty(Serienummer_private, value, (val) => { Serienummer_private = val; }, Serienummer_PropertyName); }
		}
		public static string Serienummer_PropertyName = "Serienummer";
		private string Serienummer_private;
		
		


        [NotNull]
		[Column("Datum")]

		// The actual column definition, as seen in SQLite
		public string Datum_raw { get; set; }

		public static string Datum_PropertyName = "Datum";
		
		// A helper definition that will not be saved to SQLite directly.
		// This property reads and writes to the _raw property.
		[Ignore]
		public DateTime Datum { 
			// Watch out for time zones, as they are not encoded into
			// the database. Here, I make no assumptions about time
			// zones.
			get { return Datum_raw != null ? DateTime.Parse(Datum_raw) : Datum = DateTime.Now; }
			set { SetProperty(Datum_raw, Datum_ConvertToString(value), (val) => { Datum_raw = val; }, Datum_PropertyName); }
		}

		// This static method is helpful when you need to query
		// on the raw value.
		public static string Datum_ConvertToString(DateTime date)
		{

			return date.ToString("yyyy-MM-dd");
		
		}

		


        [NotNull]
		[Column("Bedrijf")]
		public int Bedrijf 
		{ 
			get { return Bedrijf_private; }
			set { SetProperty(Bedrijf_private, value, (val) => { Bedrijf_private = val; }, Bedrijf_PropertyName); }
		}
		public static string Bedrijf_PropertyName = "Bedrijf";
		private int Bedrijf_private;
		
		


		public override string ToString() 
		{
			StringBuilder sb = new StringBuilder();


			sb.Append(ID.ToString());

			sb.Append("|");

			sb.Append(Artikel.ToString());

			sb.Append("|");

			sb.Append(Serienummer.ToString());

			sb.Append("|");

			sb.Append(Datum_ConvertToString(Datum));

			sb.Append("|");

			sb.Append(Bedrijf.ToString());

			sb.Append("|");

			return sb.ToString();
		}
	}
}
