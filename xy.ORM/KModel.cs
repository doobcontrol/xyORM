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

        #region Query

        public async Task<DataTable> SelectByPk(string pk)
        {
            string whereStr = fID + "='" + pk + "'";
            return await Select(whereStr);
        }

        public async Task<DataTable> SelectByField(string field, string value)
        {
            string whereStr = field + "='" + value + "'";
            return await Select(whereStr);
        }

        #endregion

        #region Insert

        #endregion

        #region Update

        public async Task UpdateByPk(
            Dictionary<string, string> recordDic, string pk)
        {
            string whereStr = fID + "='" + pk + "'";
            await Update(recordDic, whereStr);
        }
        #endregion

        #region Delete

        public async Task DeleteByPk(string pk)
        {
            string whereStr = fID + "='" + pk + "'";
            await Delete(whereStr);
        }

        #endregion
    }
}
