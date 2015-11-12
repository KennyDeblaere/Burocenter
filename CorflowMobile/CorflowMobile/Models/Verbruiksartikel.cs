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
	[Table("Verbruiksartikel")]
	public class Verbruiksartikel : BaseModel
	{
		public Verbruiksartikel()
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
		[Column("ArtikelID")]
		public int ArtikelID 
		{ 
			get { return ArtikelID_private; }
			set { SetProperty(ArtikelID_private, value, (val) => { ArtikelID_private = val; }, ArtikelID_PropertyName); }
		}
		public static string ArtikelID_PropertyName = "ArtikelID";
		private int ArtikelID_private;
		
		


        [NotNull]
		[Column("Gebruikt")]
		public int Gebruikt 
		{ 
			get { return Gebruikt_private; }
			set { SetProperty(Gebruikt_private, value, (val) => { Gebruikt_private = val; }, Gebruikt_PropertyName); }
		}
		public static string Gebruikt_PropertyName = "Gebruikt";
		private int Gebruikt_private;
		
		


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

			sb.Append(ArtikelID.ToString());

			sb.Append("|");

			sb.Append(Gebruikt.ToString());

			sb.Append("|");

			sb.Append(OpdrachtID.ToString());

			sb.Append("|");

			return sb.ToString();
		}
	}
}
