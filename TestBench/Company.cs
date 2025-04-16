using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xy.ORM;

namespace TestBench
{
    public class Company: KModel
    {
        #region Constructor and helper peroperty
        public Company()
        {
            _bmName = "Company";
            _bmCode = "Company";
            InitFieldList();
        }
        //helper peroperty
        public static Company i
        {
            get => BaseModel.i<Company>();
        }

        #endregion

        static public string CompanyName = "CompanyName";
        static public string CompanyCode = "CompanyCode";
        static public string CompanyType = "CompanyType";
        static public string CompanyAddress = "CompanyAddress";
        static public string CompanyPhone = "CompanyPhone";
        static public string CompanyEmail = "CompanyEmail";
        static public string CompanyWebsite = "CompanyWebsite";
        static public string CompanyFax = "CompanyFax";

        private void InitFieldList()
        {
            base.InitFieldList();

            fieldList.Add(new FieldDef(CompanyName, "CompanyName", 
                typeof(string)));
            fieldList.Add(new FieldDef(CompanyCode, "CompanyCode", 
                typeof(string)));
            fieldList.Add(new FieldDef(CompanyType, "CompanyType", 
                typeof(string)));
            fieldList.Add(new FieldDef(CompanyAddress, "CompanyAddress", 
                typeof(string)));
            fieldList.Add(new FieldDef(CompanyPhone, "CompanyPhone", 
                typeof(string)));
            fieldList.Add(new FieldDef(CompanyEmail, "CompanyEmail", 
                typeof(string)));
            fieldList.Add(new FieldDef(CompanyWebsite, "CompanyWebsite", 
                typeof(string)));
            fieldList.Add(new FieldDef(CompanyFax, "CompanyFax", 
                typeof(string)));
        }
    }
}
