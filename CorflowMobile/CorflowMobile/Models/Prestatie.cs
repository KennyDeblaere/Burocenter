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
	[Table("Prestatie")]
	public class Prestatie : BaseModel
	{
		public Prestatie()
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
		
		



		[Column("Aanvang")]

		// The actual column definition, as seen in SQLite
		public string Aanvang_raw { get; set; }

		public static string Aanvang_PropertyName = "Aanvang";
		
		// A helper definition that will not be saved to SQLite directly.
		// This property reads and writes to the _raw property.
		[Ignore]
		public Nullable<DateTime> Aanvang { 
			// Watch out for time zones, as they are not encoded into
			// the database. Here, I make no assumptions about time
			// zones.
			get { return Aanvang_raw != null ? (Nullable<DateTime>)DateTime.Parse(Aanvang_raw) : (Nullable<DateTime>)null; }
			set { SetProperty(Aanvang_raw, Aanvang_ConvertToString(value), (val) => { Aanvang_raw = val; }, Aanvang_PropertyName); }
		}

		// This static method is helpful when you need to query
		// on the raw value.
		public static string Aanvang_ConvertToString(Nullable<DateTime> date)
		{
			if (!date.HasValue)
				return null;
			else
	
			return date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
		
		}

		



		[Column("Duur")]

		// The actual column definition, as seen in SQLite
		public Nullable<long> Duur_raw { get; set; }

		// This is the static scaling factor that will be applied to convert
		// from long to decimal. 
		private static long _Duur_scale = (long)Math.Pow(10, 0);

		public static string Duur_PropertyName = "Duur";
		
		// A helper definition that will not be saved to SQLite directly.
		// This property reads and writes to the _raw property.
		[Ignore]
		public Nullable<decimal> Duur { 
			get { return Duur_raw.HasValue ? (Nullable<decimal>)((decimal)Duur_raw / (decimal)_Duur_scale) : null; }
			set { SetProperty(Duur_raw, Duur_ConvertToInt(value), (val) => { Duur_raw = val; }, Duur_PropertyName); }
		}

		// This static method is helpful when you need to query
		// on the raw value.
		public static Nullable<long> Duur_ConvertToInt(Nullable<decimal> arg_Duur)
		{
			if (!arg_Duur.HasValue)
				return null;
			else
				return (long)Math.Floor((double)(arg_Duur.Value * (decimal)_Duur_scale));
		}

		


        [NotNull]
		[Column("OpdrachtID")]
		public int OpdrachtID 
		{ 
			get { return OpdrachtID_private; }
			set { SetProperty(OpdrachtID_private, value, (val) => { OpdrachtID_private = val; }, OpdrachtID_PropertyName); }
		}
		public static string OpdrachtID_PropertyName = "OpdrachtID";
		private int OpdrachtID_private;


		public override string ToString() 
		{
			StringBuilder sb = new StringBuilder();


			sb.Append(ID.ToString());

			sb.Append("|");

			if (Aanvang != null)
			{
				sb.Append(Aanvang_ConvertToString(Aanvang));
			}
			sb.Append("|");

			if (Duur.HasValue)
			{
				sb.Append(Duur.ToString());
			}
			sb.Append("|");

			sb.Append(OpdrachtID.ToString());

			sb.Append("|");

			return sb.ToString();
		}
	}
}
