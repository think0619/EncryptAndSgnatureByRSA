using IBatisNet.DataMapper;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.DataMapper.Scope;
using IBatisNet.DataMapper.SessionStore;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace  DAL
{
    public enum IBatisActionType
    {
        Insert = 0,
        Update,
        Delete
    }


    public class ActionItem
    {
        public string IBatisSqlIdName { get; set; }

        public object DataObj { get; set; }
    }



    public class BaseDAL
    {
        static ILog log = LogManager.GetLogger(typeof(BaseDAL));
        public static void InitSqlMapper()
        {
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                iSqlMapper.SessionStore = new HybridWebThreadSessionStore(iSqlMapper.Id);
            }
        }


        public static ISqlMapper GetSqlMapper()
        {
            return Mapper.Instance();
        }

        public static int Insert<T>(string statementName, T t)
        {
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return (int)iSqlMapper.Insert(statementName, t);
            }
            return 0;
        }
        public static void InsertWithNoResult<T>(string statementName, T t)
        {
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                iSqlMapper.Insert(statementName, t);
            }
        }



        public static bool DoActionWithTransaction(Dictionary<IBatisActionType, List<ActionItem>> DataInfo, bool needBegin = true)

        //(List<string> statementList, List<object> dataList, bool needBegin = true)
        {
            bool result = false;
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                if (needBegin)
                {
                    iSqlMapper.BeginTransaction();
                }
                try
                {
                    List<ActionItem> actionList = null;
                    foreach (IBatisActionType key in DataInfo.Keys)
                    {
                        actionList = DataInfo[key];
                        switch (key)
                        {
                            case IBatisActionType.Insert:
                                
                                foreach (ActionItem item in actionList)
                                {
                                    iSqlMapper.Insert(item.IBatisSqlIdName, item.DataObj);
                                }
                                break;

                            case IBatisActionType.Update:
                                foreach (ActionItem item in actionList)
                                {
                                    iSqlMapper.Update(item.IBatisSqlIdName, item.DataObj);
                                }
                                break;
                            case IBatisActionType.Delete:
                                foreach (ActionItem item in actionList)
                                {
                                    iSqlMapper.Delete(item.IBatisSqlIdName, item.DataObj);
                                }
                                break;
                        }

                    }

                    if (needBegin)
                    {
                        iSqlMapper.CommitTransaction(true);
                    }
                    result = true;
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    if (needBegin)
                    {
                        iSqlMapper.RollBackTransaction(true);
                        log.Error("RollBackTransaction");
                    }
                    else
                    {
                       throw ex;
                    }
                }

            }

            return result;
        }





        public static bool InsertWithTransaction(List<string> statementList, List<object> dataList, bool needBegin = true)
        {
            bool result = false;
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                if (needBegin)
                {
                    iSqlMapper.BeginTransaction();
                }
                try
                {
                    for (int i = 0; i < statementList.Count; i++)
                    {
                        iSqlMapper.Insert(statementList[i], dataList[i]);
                    }
                    if (needBegin)
                    {
                        iSqlMapper.CommitTransaction(true);
                    }
                    result = true;
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    if (needBegin)
                    {
                        iSqlMapper.RollBackTransaction(true);
                        log.Error("RollBackTransaction");
                    }
                    else
                    {
                        throw ex;
                    }
                }

            }

            return result;
        }

        public static int Update<T>(string statementName, T t)
        {
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return iSqlMapper.Update(statementName, t);
            }
            return 0;
        }

        public static int Delete(string statementName, int primaryKeyId)
        {
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return iSqlMapper.Delete(statementName, primaryKeyId);
            }
            return 0;
        }
        public static int Delete(string statementName, string paramValue)
        {
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return iSqlMapper.Delete(statementName, paramValue);
            }
            return 0;
        }


        public static int Delete(string statementName, object obj)
        {
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return iSqlMapper.Delete(statementName, obj);
            }
            return 0;
        }


        //public static int Get<T>(string statementName, T t)
        //{
        //    ISqlMapper iSqlMapper = Mapper.Instance();
        //    if (iSqlMapper != null)
        //    {
        //        return Convert.ToInt16(iSqlMapper.QueryForObject(statementName, t));
        //    }
        //    return 0;
        //}

        public static int Get(string statementName, object param)
        {
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return Convert.ToInt32(iSqlMapper.QueryForObject(statementName, param));
            }
            return 0;
        }

        public static T Get<T>(string statementName, int primaryKeyId) where T : class
        {
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return iSqlMapper.QueryForObject<T>(statementName, primaryKeyId);
            }
            return null;
        }

        public static T Get<T>(string statementName, string paramValue) where T : class
        {
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return iSqlMapper.QueryForObject<T>(statementName, paramValue);
            }
            return null;
        }

        public static T Get<T>(string statementName, object paramValue) where T : class
        {
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return iSqlMapper.QueryForObject<T>(statementName, paramValue);
            }
            return null;
        }

        public static IList<T> QueryForList<T>(string statementName, object parameterObject = null)
        {
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return iSqlMapper.QueryForList<T>(statementName, parameterObject);
            }
            return null;
        }

        /// <summary>
        /// 得到运行时ibatis.net动态生成的SQL
        /// </summary>
        /// <param name="sqlMapper"></param>
        /// <param name="statementName"></param>
        /// <param name="paramObject"></param>
        /// <returns></returns>
        public static string GetRuntimeSql(string statementName, object paramObject)
        {
            string result = string.Empty;
            ISqlMapper sqlMapper = Mapper.Instance();
            if (sqlMapper == null)
            {
                return null;
            }
            else
            {
                sqlMapper.SessionStore = new HybridWebThreadSessionStore(sqlMapper.Id);
            }

            try
            {
                IMappedStatement statement = sqlMapper.GetMappedStatement(statementName);
                if (!sqlMapper.IsSessionStarted)
                {
                    sqlMapper.OpenConnection();
                }
                RequestScope scope = statement.Statement.Sql.GetRequestScope(statement, paramObject, sqlMapper.LocalSession);
                result = scope.PreparedStatement.PreparedSql;
            }
            catch (Exception ex)
            {
                result = "获取SQL语句出现异常:" + ex.Message;
            }
            return result;
        }



    }
}