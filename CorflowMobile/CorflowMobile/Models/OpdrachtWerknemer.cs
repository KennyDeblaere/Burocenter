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
	[Table("OpdrachtWerknemer")]
	public class OpdrachtWerknemer : BaseModel
	{
		public OpdrachtWerknemer()
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
		[Column("OpdrachtID")]
		public int OpdrachtID 
		{ 
			get { return OpdrachtID_private; }
			set { SetProperty(OpdrachtID_private, value, (val) => { OpdrachtID_private = val; }, OpdrachtID_PropertyName); }
		}
		public static string OpdrachtID_PropertyName = "OpdrachtID";
		private int OpdrachtID_private;
		
		


        [NotNull]
		[Column("WerknemerID")]
		public int WerknemerID 
		{ 
			get { return WerknemerID_private; }
			set { SetProperty(WerknemerID_private, value, (val) => { WerknemerID_private = val; }, WerknemerID_PropertyName); }
		}
		public static string WerknemerID_PropertyName = "WerknemerID";
		private int WerknemerID_private;
		
		



		[Column("Datum")]

		// The actual column definition, as seen in SQLite
		public string Datum_raw { get; set; }

		public static string Datum_PropertyName = "Datum";
		
		// A helper definition that will not be saved to SQLite directly.
		// This property reads and writes to the _raw property.
		[Ignore]
		public Nullable<DateTime> Datum { 
			// Watch out for time zones, as they are not encoded into
			// the database. Here, I make no assumptions about time
			// zones.
			get { return Datum_raw != null ? (Nullable<DateTime>)DateTime.Parse(Datum_raw) : (Nullable<DateTime>)null; }
			set { SetProperty(Datum_raw, Datum_ConvertToString(value), (val) => { Datum_raw = val; }, Datum_PropertyName); }
		}

		// This static method is helpful when you need to query
		// on the raw value.
		public static string Datum_ConvertToString(Nullable<DateTime> date)
		{
			if (!date.HasValue)
				return null;
			else
	
			return date.Value.ToString("yyyy-MM-dd");
		
		}

		


		public override string ToString() 
		{
			StringBuilder sb = new StringBuilder();


			sb.Append(ID.ToString());

			sb.Append("|");

			sb.Append(OpdrachtID.ToString());

			sb.Append("|");

			sb.Append(WerknemerID.ToString());

			sb.Append("|");

			if (Datum != null)
			{
				sb.Append(Datum_ConvertToString(Datum));
			}
			sb.Append("|");

			return sb.ToString();
		}
	}
}
