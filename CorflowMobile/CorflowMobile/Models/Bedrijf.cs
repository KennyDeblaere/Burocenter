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
	[Table("Bedrijf")]
	public class Bedrijf : BaseModel
	{
		public Bedrijf()
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
		[Column("BTW")]
		public string BTW 
		{ 
			get { return BTW_private; }
			set { SetProperty(BTW_private, value, (val) => { BTW_private = val; }, BTW_PropertyName); }
		}
		public static string BTW_PropertyName = "BTW";
		private string BTW_private;
		
		


        [NotNull]
		[Column("Naam")]
		public string Naam 
		{ 
			get { return Naam_private; }
			set { SetProperty(Naam_private, value, (val) => { Naam_private = val; }, Naam_PropertyName); }
		}
		public static string Naam_PropertyName = "Naam";
		private string Naam_private;
		
		


        [NotNull]
		[Column("Adres")]
		public int Adres 
		{ 
			get { return Adres_private; }
			set { SetProperty(Adres_private, value, (val) => { Adres_private = val; }, Adres_PropertyName); }
		}
		public static string Adres_PropertyName = "Adres";
		private int Adres_private;
		
		


		public override string ToString() 
		{
			StringBuilder sb = new StringBuilder();


			sb.Append(ID.ToString());

			sb.Append("|");

			sb.Append(BTW.ToString());

			sb.Append("|");

			sb.Append(Naam.ToString());

			sb.Append("|");

			sb.Append(Adres.ToString());

			sb.Append("|");

			return sb.ToString();
		}
	}
}
