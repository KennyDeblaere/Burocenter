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
	[Table("Adres")]
	public class Adres : BaseModel
	{
		public Adres()
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
		[Column("Straat")]
		public string Straat 
		{ 
			get { return Straat_private; }
			set { SetProperty(Straat_private, value, (val) => { Straat_private = val; }, Straat_PropertyName); }
		}
		public static string Straat_PropertyName = "Straat";
		private string Straat_private;
		
		



		[Column("Nummer")]
		public string Nummer 
		{ 
			get { return Nummer_private; }
			set { SetProperty(Nummer_private, value, (val) => { Nummer_private = val; }, Nummer_PropertyName); }
		}
		public static string Nummer_PropertyName = "Nummer";
		private string Nummer_private;
		
		


        [NotNull]
		[Column("Gemeente")]
		public int Gemeente 
		{ 
			get { return Gemeente_private; }
			set { SetProperty(Gemeente_private, value, (val) => { Gemeente_private = val; }, Gemeente_PropertyName); }
		}
		public static string Gemeente_PropertyName = "Gemeente";
		private int Gemeente_private;
		
		


		public override string ToString() 
		{
			StringBuilder sb = new StringBuilder();


			sb.Append(ID.ToString());

			sb.Append("|");

			sb.Append(Straat.ToString());

			sb.Append("|");

			if (Nummer != null)
			{
				sb.Append(Nummer.ToString());
			}
			sb.Append("|");

			sb.Append(Gemeente.ToString());

			sb.Append("|");

			return sb.ToString();
		}
	}
}
