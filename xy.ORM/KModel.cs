using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xy.ORM
{
    public abstract class KModel : BaseModel
    {
        static public String fID = "fID";
        protected void InitFieldList()
        {
            base.InitFieldList();

            fieldList.Add(new FieldDef(fID, "Key",
                typeof(string), true));
        }
    }
}
