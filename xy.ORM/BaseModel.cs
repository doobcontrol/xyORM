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
                //instance.InitFieldList();
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
                }
                dbScript += model.createTableDbScript();
            }

            return dbScript;
        }
        private string createTableDbScript()
        {
            string tabledbScript = "CREATE TABLE " + _bmCode + "(" +
                createFieldDbScript() + ");";

            return tabledbScript;
        }
        private string createFieldDbScript()
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
            return fieldDbScript;
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
