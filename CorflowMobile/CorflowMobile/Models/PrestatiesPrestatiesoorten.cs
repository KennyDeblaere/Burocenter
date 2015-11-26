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
    [Table("PrestatiesPrestatiesoorten")]
    public class PrestatiesPrestatiesoorten : BaseModel
    {
        public PrestatiesPrestatiesoorten()
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
        [Column("PrestatieID")]
        public int PrestatieID
        {
            get { return PrestatieID_private; }
            set { SetProperty(PrestatieID_private, value, (val) => { PrestatieID_private = val; }, PrestatieID_PropertyName); }
        }
        public static string PrestatieID_PropertyName = "PrestatieID";
        private int PrestatieID_private;




        [NotNull]
        [Column("PrestatieSoortID")]
        public int PrestatieSoortID
        {
            get { return PrestatieSoortID_private; }
            set { SetProperty(PrestatieSoortID_private, value, (val) => { PrestatieSoortID_private = val; }, PrestatieSoortID_PropertyName); }
        }
        public static string PrestatieSoortID_PropertyName = "PrestatieSoortID";
        private int PrestatieSoortID_private;




        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();


            sb.Append(ID.ToString());

            sb.Append("|");

            sb.Append(PrestatieID.ToString());

            sb.Append("|");

            sb.Append(PrestatieSoortID.ToString());

            sb.Append("|");

            return sb.ToString();
        }
    }
}