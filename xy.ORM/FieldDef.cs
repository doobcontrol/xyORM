using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xy.ORM
{
    public class FieldDef
    {
        private string _fieldName;
        private Type _fieldType;
        private int _fieldLength = -1;
        private string _fieldCode;
        private bool _isPrimaryKey = false;
        private bool _isForeignKey = false;
        private bool _isNullable = true;
        private KModel _refModel;
        public string FieldName { get => _fieldName; }
        public string FieldCode { get => _fieldCode; }
        public Type FieldType { get => _fieldType; }
        public bool IsPrimaryKey { get => _isPrimaryKey; }
        public bool IsForeignKey { get => _isForeignKey; }
        public bool IsNullable { get => _isNullable; }
        public KModel RefModel { get => _refModel; }
        public FieldDef(
            string fieldCode, 
            string fieldName, 
            Type fieldType)
        {
            _fieldName = fieldName;
            _fieldCode = fieldCode;
            _fieldType = fieldType;
        }
        public FieldDef(
            string fieldCode,
            string fieldName,
            Type fieldType,
            int fieldLength) : this(fieldCode, fieldName, fieldType)
        {
            _fieldLength = fieldLength;
        }
        public FieldDef(
            string fieldCode, 
            string fieldName, 
            Type fieldType,
            bool isPrimaryKey): this(fieldCode, fieldName, fieldType)
        {
            _isPrimaryKey = isPrimaryKey;
        }
        public FieldDef(
            string fieldCode,
            string fieldName,
            KModel refModel) : this(fieldCode, fieldName, typeof(string))
        {
            _isForeignKey = true;
            _refModel = refModel;
        }

        public string createDbScript()
        {
            string dbScript = _fieldCode + " ";

            //get data type Name from each DBMS??
            dbScript += getDatatypeDbString() + " ";

            if (IsPrimaryKey)
            {
                dbScript += "PRIMARY KEY ";
            }
            if (!IsNullable || IsPrimaryKey)
            {
                dbScript += "NOT NULL";
            }

            return dbScript;
        }
        public string getDatatypeDbString()
        {
            string fieldTypeString = string.Empty;
            switch (_fieldType)
            {
                case Type t when t == typeof(string):
                    int length = _fieldLength;
                    if (_fieldLength < 0)
                    {
                        length = 100; ;
                    }
                    fieldTypeString = "VARCHAR("+ length + ")";
                    break;
                case Type t when t == typeof(int):
                    fieldTypeString = "INT";
                    break;
                case Type t when t == typeof(float):
                    fieldTypeString = "REAL";
                    break;
                case Type t when t == typeof(double):
                    fieldTypeString = "DOUBLE";
                    break;
                case Type t when t == typeof(DateTime):
                    fieldTypeString = "DATETIME";
                    break;
                default:
                    throw new Exception("Unknown data type");
            }
            return fieldTypeString;
        }
    }
}
