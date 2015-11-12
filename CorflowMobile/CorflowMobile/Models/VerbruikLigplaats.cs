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
	[Table("VerbruikLigplaats")]
	public class VerbruikLigplaats : BaseModel
	{
		public VerbruikLigplaats()
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
		[Column("LigplaatsID")]
		public int LigplaatsID 
		{ 
			get { return LigplaatsID_private; }
			set { SetProperty(LigplaatsID_private, value, (val) => { LigplaatsID_private = val; }, LigplaatsID_PropertyName); }
		}
		public static string LigplaatsID_PropertyName = "LigplaatsID";
		private int LigplaatsID_private;
		
		


        [NotNull]
		[Column("AantalStock")]
		public int AantalStock 
		{ 
			get { return AantalStock_private; }
			set { SetProperty(AantalStock_private, value, (val) => { AantalStock_private = val; }, AantalStock_PropertyName); }
		}
		public static string AantalStock_PropertyName = "AantalStock";
		private int AantalStock_private;
		
		


        [NotNull]
		[Column("MinStock")]
		public int MinStock 
		{ 
			get { return MinStock_private; }
			set { SetProperty(MinStock_private, value, (val) => { MinStock_private = val; }, MinStock_PropertyName); }
		}
		public static string MinStock_PropertyName = "MinStock";
		private int MinStock_private;
		
		


        [NotNull]
		[Column("MaxStock")]
		public int MaxStock 
		{ 
			get { return MaxStock_private; }
			set { SetProperty(MaxStock_private, value, (val) => { MaxStock_private = val; }, MaxStock_PropertyName); }
		}
		public static string MaxStock_PropertyName = "MaxStock";
		private int MaxStock_private;
		
		


		public override string ToString() 
		{
			StringBuilder sb = new StringBuilder();


			sb.Append(ID.ToString());

			sb.Append("|");

			sb.Append(ArtikelID.ToString());

			sb.Append("|");

			sb.Append(LigplaatsID.ToString());

			sb.Append("|");

			sb.Append(AantalStock.ToString());

			sb.Append("|");

			sb.Append(MinStock.ToString());

			sb.Append("|");

			sb.Append(MaxStock.ToString());

			sb.Append("|");

			return sb.ToString();
		}
	}
}
