using System.Xml.Linq;
using System;
using xy.Db;

namespace xy.ORM
{
    public abstract class BaseModel
    {
        #region DbService

        private static DbService defaultDbService;
        public static DbService DefaultDbService { 
            get => defaultDbService; 
            set => defaultDbService = value; 
        }

        private DbService _dbService;
        public DbService DbService { 
            get => _dbService; 
            set => _dbService = value; 
        }

        #endregion

        #region Model and Object instance manage

        private static Dictionary<Type, BaseModel> _instanceDic 
            = new Dictionary<Type, BaseModel>();
        public static T i<T>() where T : BaseModel, new()
        {
            if (_instanceDic.ContainsKey(typeof(T)))
            {
                return (T)_instanceDic[typeof(T)];
            }
            else
            {
                T instance = new T();
                _instanceDic.Add(typeof(T), instance);
                return instance;
            }
        }

        private static List<Type> modelList
            = new List<Type>();
        public static void addToModelList(Type type)
        {
            if(!modelList.Contains(type))
            {
                modelList.Add(type);
            }
        }

        #endregion

        #region DataBase map

        protected string _bmName; // table show name
        protected string _bmCode; // table name
        public string BmCode { get => _bmCode; }

        #region fields

        protected List<FieldDef> fieldList;
        public virtual List<FieldDef> FieldList { get => fieldList; }

        protected virtual void InitFieldList()
        {
            fieldList = new List<FieldDef>();
        }

        #endregion

        #endregion

        #region Sql 

        #region DataBase Init

        public static async Task CreateDatabaseAsync(
            Dictionary<string, string> dbCreatePars,
            Dictionary<string, string> adminPars
            )
        {
            string dbScript = createDbScript();
            dbCreatePars.Add(DbService.pn_dbScript, dbScript);
            await DefaultDbService.OpenForAdminAsync(adminPars);
            string createdConnectString =
                await DefaultDbService.DbCreateAsync(dbCreatePars);
        }
        private static string createDbScript()
        {
            string dbScript = "";

            //table script
            foreach(Type type in modelList)
            {
                BaseModel model;
                if (_instanceDic.ContainsKey(type))
                {
                    model = _instanceDic[type];
                }
                else
                {
                    model = (BaseModel)Activator.CreateInstance(type);
                    _instanceDic[type] = model;
                }
                dbScript += model.createTableDbScript();
            }

            //foreign Key script
            if (DefaultDbService.createForeignKeyAfterCreateTable())
            {
                foreach (Type type in modelList)
                {
                    dbScript += _instanceDic[type]
                        .createForeignKeyDbScript();
                }
            }

            return dbScript;
        }
        private string createTableDbScript()
        {
            string tabledbScript = "CREATE TABLE " + BmCode + "(" +
                createFieldDbScript(
                    DefaultDbService.createForeignKeyWhenCreateTable()
                    ) + ");";

            return tabledbScript;
        }
        private string createFieldDbScript(bool createForeignKey)
        {
            string fieldDbScript = "";
            foreach (FieldDef field in fieldList)
            {
                if(fieldDbScript != "")
                {
                    fieldDbScript += ",";
                }
                fieldDbScript += field.createDbScript();
            }
            if(createForeignKey) {
                foreach (FieldDef field in fieldList)
                {
                    //??
                }
            }
            return fieldDbScript;
        }
        private string createForeignKeyDbScript()
        {
            string constraints  = "";
            foreach (FieldDef field in fieldList)
            {
                if (field.IsForeignKey)
                {
                    if (constraints != "")
                    {
                        constraints += ",";
                    }
                    constraints += "ADD CONSTRAINT ";
                    constraints += _bmCode 
                        + field.FieldCode + field.RefModel.BmCode + " ";
                    constraints += "FOREIGN KEY (" + field.FieldCode + ") ";
                    constraints += "REFERENCES " + field.RefModel.BmCode + "(";
                    constraints += KModel.fID + ")";
                }
            }
            string foreignKeyDbScript = "";
            if(constraints != "")
            {
                foreignKeyDbScript += "ALTER TABLE ";
                foreignKeyDbScript += _bmCode + " ";
                foreignKeyDbScript += constraints + ";";
            }
            return foreignKeyDbScript;
        }


        #endregion

        #region Data

        #endregion
        public void update()
        {

        }

        #endregion
    }
}
