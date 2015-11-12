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
	[Table("Ligplaats")]
	public class Ligplaats : BaseModel
	{
		public Ligplaats()
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
		[Column("Omschrijving")]
		public string Omschrijving 
		{ 
			get { return Omschrijving_private; }
			set { SetProperty(Omschrijving_private, value, (val) => { Omschrijving_private = val; }, Omschrijving_PropertyName); }
		}
		public static string Omschrijving_PropertyName = "Omschrijving";
		private string Omschrijving_private;
		
		



		[Column("HoofdligplaatsID")]
		public Nullable<int> HoofdligplaatsID 
		{ 
			get { return HoofdligplaatsID_private; }
			set { SetProperty(HoofdligplaatsID_private, value, (val) => { HoofdligplaatsID_private = val; }, HoofdligplaatsID_PropertyName); }
		}
		public static string HoofdligplaatsID_PropertyName = "HoofdligplaatsID";
		private Nullable<int> HoofdligplaatsID_private;
		
		


        [NotNull]
		[Column("VerantwoordelijkeLigplaatsID")]
		public int VerantwoordelijkeLigplaatsID 
		{ 
			get { return VerantwoordelijkeLigplaatsID_private; }
			set { SetProperty(VerantwoordelijkeLigplaatsID_private, value, (val) => { VerantwoordelijkeLigplaatsID_private = val; }, VerantwoordelijkeLigplaatsID_PropertyName); }
		}
		public static string VerantwoordelijkeLigplaatsID_PropertyName = "VerantwoordelijkeLigplaatsID";
		private int VerantwoordelijkeLigplaatsID_private;
		
		


		public override string ToString() 
		{
			StringBuilder sb = new StringBuilder();


			sb.Append(ID.ToString());

			sb.Append("|");

			sb.Append(Omschrijving.ToString());

			sb.Append("|");

			if (HoofdligplaatsID.HasValue)
			{
				sb.Append(HoofdligplaatsID.ToString());
			}
			sb.Append("|");

			sb.Append(VerantwoordelijkeLigplaatsID.ToString());

			sb.Append("|");

			return sb.ToString();
		}
	}
}
