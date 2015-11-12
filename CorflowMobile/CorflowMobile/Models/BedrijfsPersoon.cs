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
	[Table("BedrijfsPersoon")]
	public class BedrijfsPersoon : BaseModel
	{
		public BedrijfsPersoon()
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
		[Column("PersoonID")]
		public int PersoonID 
		{ 
			get { return PersoonID_private; }
			set { SetProperty(PersoonID_private, value, (val) => { PersoonID_private = val; }, PersoonID_PropertyName); }
		}
		public static string PersoonID_PropertyName = "PersoonID";
		private int PersoonID_private;
		
		


        [NotNull]
		[Column("BedrijfID")]
		public int BedrijfID 
		{ 
			get { return BedrijfID_private; }
			set { SetProperty(BedrijfID_private, value, (val) => { BedrijfID_private = val; }, BedrijfID_PropertyName); }
		}
		public static string BedrijfID_PropertyName = "BedrijfID";
		private int BedrijfID_private;
		
		


		public override string ToString() 
		{
			StringBuilder sb = new StringBuilder();


			sb.Append(ID.ToString());

			sb.Append("|");

			sb.Append(PersoonID.ToString());

			sb.Append("|");

			sb.Append(BedrijfID.ToString());

			sb.Append("|");

			return sb.ToString();
		}
	}
}
