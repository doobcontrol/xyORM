using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xy.ORM;

namespace TestBench
{
    public class Company: BaseModel
    {
        public Company()
        {
            _bmName = "公司";
            _bmCode = "Company";
            InitFieldList();
        }
        //helper peroperty
        public static Company i
        {
            get => BaseModel.i<Company>();
        }

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

            fieldList.Add(new FieldDef(CompanyName, "公司名称", 
                typeof(string)));
            fieldList.Add(new FieldDef(CompanyCode, "公司代码", 
                typeof(string)));
            fieldList.Add(new FieldDef(CompanyType, "公司类型", 
                typeof(string)));
            fieldList.Add(new FieldDef(CompanyAddress, "公司地址", 
                typeof(string)));
            fieldList.Add(new FieldDef(CompanyPhone, "公司电话", 
                typeof(string)));
            fieldList.Add(new FieldDef(CompanyEmail, "公司邮箱", 
                typeof(string)));
            fieldList.Add(new FieldDef(CompanyWebsite, "公司网站", 
                typeof(string)));
            fieldList.Add(new FieldDef(CompanyFax, "公司传真", 
                typeof(string)));
        }
    }
}
