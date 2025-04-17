﻿using System.Xml.Linq;
using System;
using xy.Db;
using System.Data;

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
                instance.initNewInstance();
                return instance;
            }
        }
        private void initNewInstance()
        {
            Type type = this.GetType();
            if (_instanceDic.ContainsKey(type))
            {
                _instanceDic[type] = this;
            }
            else
            {
                _instanceDic.Add(type, this);
            }
            this.DbService = defaultDbService;
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

        public static async Task<string> CreateDatabaseAsync(
            Dictionary<string, string> dbCreatePars,
            Dictionary<string, string> adminPars
            )
        {
            string dbScript = createDbScript();
            dbCreatePars.Add(DbService.pn_dbScript, dbScript);
            await DefaultDbService.OpenForAdminAsync(adminPars);
            string createdConnectString =
                await DefaultDbService.DbCreateAsync(dbCreatePars);
            return createdConnectString;
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
                    model.initNewInstance();
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
                    if (field.IsForeignKey)
                    {
                        fieldDbScript += ",CONSTRAINT ";
                        fieldDbScript += _bmCode
                            + field.FieldCode + field.RefModel.BmCode + " ";
                        fieldDbScript += "FOREIGN KEY (" 
                            + field.FieldCode + ") ";
                        fieldDbScript += "REFERENCES " 
                            + field.RefModel.BmCode + "(";
                        fieldDbScript += KModel.fID + ") ";
                    }
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
        public async Task<DataTable> QueryAsync(string sql)
        {
            DataTable dt = await DbService.exeSqlForDataSetAsync(sql);
            return dt;
        }
        public async Task EditAsync(string sql)
        {
            await DbService.exeSqlAsync(sql);
        }

        #region Query

        public async Task<DataTable> SelectAll()
        {
            string sql = "SELECT * FROM " + _bmCode;
            return await QueryAsync(sql);
        }

        public async Task<DataTable> Select(string whereStr)
        {
            string sql = "SELECT * FROM " + _bmCode + " WHERE " + whereStr;
            return await QueryAsync(sql);
        }

        #endregion

        #region Insert
        public async Task Insert(Dictionary<string, string> recordDic)
        {
            string fStr = "";
            string vStr = "";
            foreach (KeyValuePair<string, string> kvp in recordDic)
            {
                if (fStr != "")
                {
                    fStr += ",";
                    vStr += ",";
                }
                fStr += kvp.Key;
                vStr += "'" + kvp.Value + "'";
            }
            string sql = "INSERT INTO " + _bmCode 
                + "("+ fStr + ") VALUES("+ vStr + ")";
            await EditAsync(sql);
        }

        #endregion

        #region Update

        public async Task Update(
            Dictionary<string, string> recordDic, string whereStr)
        {
            string sStr = "";
            foreach (KeyValuePair<string, string> kvp in recordDic)
            {
                if (sStr != "")
                {
                    sStr += ",";
                }
                sStr += kvp.Key + "='" + kvp.Value + "'";
            }
            string sql = "UPDATE " + _bmCode
                + " SET " + sStr + " WHERE " + whereStr;
            await EditAsync(sql);
        }

        #endregion

        #region Delete

        public async Task Delete(string whereStr)
        {
            string sql = "DELETE FROM " + _bmCode
                + " WHERE " + whereStr;
            await EditAsync(sql);
        }

        #endregion

        #endregion

        #endregion
    }
}
