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
	[Table("Verslag")]
	public class Verslag : BaseModel
	{
		public Verslag()
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
		[Column("Titel")]
		public string Titel 
		{ 
			get { return Titel_private; }
			set { SetProperty(Titel_private, value, (val) => { Titel_private = val; }, Titel_PropertyName); }
		}
		public static string Titel_PropertyName = "Titel";
		private string Titel_private;
		
		


        [NotNull]
		[Column("Verslag")]
		public string verslag 
		{ 
			get { return Verslag_private; }
			set { SetProperty(Verslag_private, value, (val) => { Verslag_private = val; }, Verslag_PropertyName); }
		}
		public static string Verslag_PropertyName = "Verslag";
		private string Verslag_private;
		
		


        [NotNull]
		[Column("Prestatie")]
		public int Prestatie 
		{ 
			get { return Prestatie_private; }
			set { SetProperty(Prestatie_private, value, (val) => { Prestatie_private = val; }, Prestatie_PropertyName); }
		}
		public static string Prestatie_PropertyName = "Prestatie";
		private int Prestatie_private;
		
		


		public override string ToString() 
		{
			StringBuilder sb = new StringBuilder();


			sb.Append(ID.ToString());

			sb.Append("|");

			sb.Append(Titel.ToString());

			sb.Append("|");

			sb.Append(verslag.ToString());

			sb.Append("|");

			sb.Append(Prestatie.ToString());

			sb.Append("|");

			return sb.ToString();
		}
	}
}
