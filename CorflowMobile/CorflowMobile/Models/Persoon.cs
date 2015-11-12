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
	[Table("Persoon")]
	public class Persoon : BaseModel
	{
		public Persoon()
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
		[Column("Voornaam")]
		public string Voornaam 
		{ 
			get { return Voornaam_private; }
			set { SetProperty(Voornaam_private, value, (val) => { Voornaam_private = val; }, Voornaam_PropertyName); }
		}
		public static string Voornaam_PropertyName = "Voornaam";
		private string Voornaam_private;
		
		


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
		[Column("TelefoonWerk")]
		public string TelefoonWerk 
		{ 
			get { return TelefoonWerk_private; }
			set { SetProperty(TelefoonWerk_private, value, (val) => { TelefoonWerk_private = val; }, TelefoonWerk_PropertyName); }
		}
		public static string TelefoonWerk_PropertyName = "TelefoonWerk";
		private string TelefoonWerk_private;
		
		



		[Column("TelefoonPrive")]
		public string TelefoonPrive 
		{ 
			get { return TelefoonPrive_private; }
			set { SetProperty(TelefoonPrive_private, value, (val) => { TelefoonPrive_private = val; }, TelefoonPrive_PropertyName); }
		}
		public static string TelefoonPrive_PropertyName = "TelefoonPrive";
		private string TelefoonPrive_private;
		
		



		[Column("Fax")]
		public string Fax 
		{ 
			get { return Fax_private; }
			set { SetProperty(Fax_private, value, (val) => { Fax_private = val; }, Fax_PropertyName); }
		}
		public static string Fax_PropertyName = "Fax";
		private string Fax_private;
		
		


        [NotNull]
		[Column("Email")]
		public string Email 
		{ 
			get { return Email_private; }
			set { SetProperty(Email_private, value, (val) => { Email_private = val; }, Email_PropertyName); }
		}
		public static string Email_PropertyName = "Email";
		private string Email_private;
		
		


        [NotNull]
		[Column("Actief")]
		public bool Actief 
		{ 
			get { return Actief_private; }
			set { SetProperty(Actief_private, value, (val) => { Actief_private = val; }, Actief_PropertyName); }
		}
		public static string Actief_PropertyName = "Actief";
		private bool Actief_private;
		
		


        [NotNull]
		[Column("Functie")]
		public int Functie 
		{ 
			get { return Functie_private; }
			set { SetProperty(Functie_private, value, (val) => { Functie_private = val; }, Functie_PropertyName); }
		}
		public static string Functie_PropertyName = "Functie";
		private int Functie_private;
		
		


		public override string ToString() 
		{
			StringBuilder sb = new StringBuilder();


			sb.Append(ID.ToString());

			sb.Append("|");

			sb.Append(Voornaam.ToString());

			sb.Append("|");

			sb.Append(Naam.ToString());

			sb.Append("|");

			sb.Append(TelefoonWerk.ToString());

			sb.Append("|");

			if (TelefoonPrive != null)
			{
				sb.Append(TelefoonPrive.ToString());
			}
			sb.Append("|");

			if (Fax != null)
			{
				sb.Append(Fax.ToString());
			}
			sb.Append("|");

			sb.Append(Email.ToString());

			sb.Append("|");

			sb.Append(Actief.ToString());

			sb.Append("|");

			sb.Append(Functie.ToString());

			sb.Append("|");

			return sb.ToString();
		}
	}
}
