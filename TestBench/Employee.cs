using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xy.ORM;

namespace TestBench
{
    public class Employee : KModel
    {
        #region Constructor and helper peroperty
        public Employee()
        {
            _bmName = "Employee";
            _bmCode = "Employee";
            InitFieldList();
        }
        //helper peroperty
        public static Employee i
        {
            get => BaseModel.i<Employee>();
        }

        #endregion
    
        static public string EmployeeName = "EmployeeName";
        static public string CompanyID = "CompanyID";
        static public string EmployeeCode = "EmployeeCode";
        static public string EmployeeType = "EmployeeType";
        static public string EmployeeAddress = "EmployeeAddress";

        private void InitFieldList()
        {
            base.InitFieldList();

            fieldList.Add(new FieldDef(CompanyID, "CompanyID",
                Company.i));
            fieldList.Add(new FieldDef(EmployeeName, "EmployeeName",
                typeof(string)));
            fieldList.Add(new FieldDef(EmployeeCode, "EmployeeCode",
                typeof(string)));
            fieldList.Add(new FieldDef(EmployeeType, "EmployeeType",
                typeof(string)));
            fieldList.Add(new FieldDef(EmployeeAddress, "EmployeeAddress",
                typeof(string)));
        }

    }
}
