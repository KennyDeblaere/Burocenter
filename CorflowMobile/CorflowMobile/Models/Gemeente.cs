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
	[Table("Gemeente")]
	public class Gemeente : BaseModel
	{
		public Gemeente()
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
		[Column("Postcode")]
		public string Postcode 
		{ 
			get { return Postcode_private; }
			set { SetProperty(Postcode_private, value, (val) => { Postcode_private = val; }, Postcode_PropertyName); }
		}
		public static string Postcode_PropertyName = "Postcode";
		private string Postcode_private;
		
		


        [NotNull]
		[Column("Plaats")]
		public string Plaats 
		{ 
			get { return Plaats_private; }
			set { SetProperty(Plaats_private, value, (val) => { Plaats_private = val; }, Plaats_PropertyName); }
		}
		public static string Plaats_PropertyName = "Plaats";
		private string Plaats_private;
		
		


		public override string ToString() 
		{
			StringBuilder sb = new StringBuilder();


			sb.Append(ID.ToString());

			sb.Append("|");

			sb.Append(Postcode.ToString());

			sb.Append("|");

			sb.Append(Plaats.ToString());

			sb.Append("|");

			return sb.ToString();
		}
	}
}
