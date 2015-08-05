using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;

using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;

namespace CEMS_WS
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class cems : System.Web.Services.WebService
    {
        #region temporary
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>
        /// add new pi value--local
        /// </summary>
        /// <param name="ts">timestamp</param>
        /// <param name="pn">point name</param>
        /// <param name="pv">point value</param>
        /// <param name="mi">machine id</param>
        /// <param name="pi">plant id</param>
        /// <returns>0 for success</returns>
        [WebMethod]
        public int AddPiRecord(DateTime ts, string pn, float pv, int mi, int pi)
        {
            try
            {
                if (IsPiRdExisted(ts, pn, mi, pi) == false)
                {
                    //not existed
                    StringBuilder sb = new StringBuilder();
                    sb.Append("insert into PiRecords(pname,timestamps,pvalue,updatetime,machineid,plantid) values('");
                    sb.Append(pn.ToString() + "','");
                    sb.Append(ts.ToString("yyyy-MM-dd HH:mm:ss") + "',");
                    sb.Append(pv.ToString() + ",'");
                    sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
                    sb.Append(mi.ToString() + ",");
                    sb.Append(pi.ToString() + ")");
                    Database db = DatabaseFactory.CreateDatabase("dbconn");
                    System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                    db.ExecuteNonQuery(dbc);
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// add new cems rule result--local
        /// </summary>
        /// <param name="ts">timestamp</param>
        /// <param name="pn">point name</param>
        /// <param name="rn">rule name</param>
        /// <param name="rt">rule type</param>
        /// <param name="rd">rule description</param>
        /// <param name="mi">machine id</param>
        /// <param name="pi">plant id</param>
        /// <returns>0 for success</returns>
        [WebMethod]
        public int AddRuleLogRd(DateTime ts, string pn, string rn, string rt, string rd, int mi, int pi)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("insert into CEMSRuleLogRecords(timestamps,pname,rname,ruletype,rulediscrip,updatetime,machineid,plantid) values('");
                sb.Append(ts.ToString("yyyy-MM-dd HH:mm:ss") + "','");
                sb.Append(pn.ToString() + "','");
                sb.Append(rn.ToString() + "','");
                sb.Append(rt.ToString() + "','");
                sb.Append(rd.ToString() + "','");
                sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
                sb.Append(mi.ToString() + ",");
                sb.Append(pi.ToString() + ")");
                Database db = DatabaseFactory.CreateDatabase("dbconn");
                System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                db.ExecuteNonQuery(dbc);
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// update existed pi rd or add new pi rd
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="pn"></param>
        /// <param name="pv"></param>
        /// <param name="mi"></param>
        /// <param name="pi"></param>
        /// <returns>0 for success</returns>
        [WebMethod]
        public int UpdatePiRecord(DateTime ts, string pn, float pv, int mi, int pi)
        {
            try
            {
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// update existed rule result or add new rule result
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="pn"></param>
        /// <param name="rn"></param>
        /// <param name="rt"></param>
        /// <param name="rd"></param>
        /// <param name="mi"></param>
        /// <param name="pi"></param>
        /// <returns>0 for success</returns>
        [WebMethod]
        public int UpdateRuleLogRd(DateTime ts, string pn, string rn, string rt, string rd, int mi, int pi)
        {
            try
            {
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// determine whether pi rd existed--local
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="pn"></param>
        /// <param name="mi"></param>
        /// <param name="pi"></param>
        /// <returns>null for failure</returns>
        public bool? IsPiRdExisted(DateTime ts, string pn, int mi, int pi)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select count(*) from PiRecords t where ");
                sb.Append("t.pname = '" + pn + "' and ");
                sb.Append("t.timestamps = '" + ts.ToString("yyyy-MM-dd HH:mm:ss") + "' and ");
                sb.Append("t.machineid =" + mi.ToString() + " and ");
                sb.Append("t.plantid=" + pi.ToString());
                Database db = DatabaseFactory.CreateDatabase("dbconn");
                System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                if ((int)db.ExecuteScalar(dbc) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// determine whether cems rd existed
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="pn"></param>
        /// <param name="rn"></param>
        /// <param name="rt"></param>
        /// <param name="rd"></param>
        /// <param name="mi"></param>
        /// <param name="pi"></param>
        /// <returns>null for failure</returns>
        public bool? IsCemsRdExisted(DateTime ts, string pn, string rn, string rt, string rd, int mi, int pi)
        {
            try
            {
                return false;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// delete pi rd
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="pn"></param>
        /// <param name="mi"></param>
        /// <param name="pi"></param>
        /// <returns>true for success</returns>
        public bool DeletePiRd(DateTime ts, string pn, int mi, int pi)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// delete cems rd
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="pn"></param>
        /// <param name="rn"></param>
        /// <param name="rt"></param>
        /// <param name="rd"></param>
        /// <param name="mi"></param>
        /// <param name="pi"></param>
        /// <returns>true for success</returns>
        public bool DeleteCemsRd(DateTime ts, string pn, string rn, string rt, string rd, int mi, int pi)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region pi data
        /// <summary>
        /// 中心端--pi数据插入
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="ts"></param>
        /// <param name="pn"></param>
        /// <param name="pv"></param>
        /// <param name="mi"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        [WebMethod]
        public int AddPiRecord_c(long lid, DateTime ts, string pn, float pv, int mi, int pi)
        {
            try
            {
                if (IsPiRdExisted_c(lid, pi) == false)
                {
                    //not existed
                    StringBuilder sb = new StringBuilder();
                    sb.Append("insert into PiRecords(pname,timestamps,pvalue,updatetime,machineid,plantid,id) values('");
                    sb.Append(pn.ToString() + "','");
                    sb.Append(ts.ToString("yyyy-MM-dd HH:mm:ss") + "',");
                    sb.Append(pv.ToString() + ",'");
                    sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
                    sb.Append(mi.ToString() + ",");
                    sb.Append(pi.ToString() + ",");
                    sb.Append(lid.ToString() + ")");
                    Database db = DatabaseFactory.CreateDatabase("dbconn");
                    System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                    db.ExecuteNonQuery(dbc);
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// 中心端--determine whether pi rd existed
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="ts"></param>
        /// <param name="pn"></param>
        /// <param name="mi"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        public bool? IsPiRdExisted_c(long lid, int pi)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select count(*) from PiRecords t where ");
                sb.Append("t.plantid=" + pi.ToString() + " and ");
                sb.Append("t.id=" + lid.ToString());
                Database db = DatabaseFactory.CreateDatabase("dbconn");
                System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                if ((int)db.ExecuteScalar(dbc) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// update pi records
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="ts"></param>
        /// <param name="pn"></param>
        /// <param name="pv"></param>
        /// <param name="mi"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        [WebMethod]
        public int UpdatePiRecord_c(long lid, DateTime ts, string pn, float pv, int mi, int pi)
        {
            try
            {
                if (IsPiRdExisted_c(lid, pi) == false)
                {
                    //not existed
                    AddPiRecord_c(lid, ts, pn, pv, mi, pi);
                    return 0;
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("update PiRecords set pname='");
                    sb.Append(pn.ToString() + "',timestamps='");
                    sb.Append(ts.ToString("yyyy-MM-dd HH:mm:ss") + "',pvalue=");
                    sb.Append(pv.ToString() + ",updatetime='");
                    sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',machineid=");
                    sb.Append(mi.ToString() + " where plantid=");
                    sb.Append(pi.ToString() + " and id=");
                    sb.Append(lid.ToString());
                    Database db = DatabaseFactory.CreateDatabase("dbconn");
                    System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                    db.ExecuteNonQuery(dbc);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        #endregion

        #region rule log

        /// <summary>
        /// 中心端--add new cems rule result
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="ts"></param>
        /// <param name="pn"></param>
        /// <param name="rt"></param>
        /// <param name="rd"></param>
        /// <param name="cemstype"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        [WebMethod]
        public int AddRuleLogRd_c(long lid, DateTime ts, string pn, /*string rn,*/ string rt, string rd, string cemstype,/*int mi,*/ int pi)
        {
            try
            {
                if (IsRuleLogExisted_c(lid, pi) == false)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("insert into t_RulelogS(timelog,rulename,alarmlog,alarmdis,cemstype,plantid,id) values('");
                    sb.Append(ts.ToString("yyyy-MM-dd HH:mm:ss") + "','");
                    sb.Append(pn.ToString() + "','");
                    sb.Append(rt.ToString() + "','");
                    sb.Append(rd.ToString() + "','");
                    sb.Append(cemstype.ToString() + "',");
                    sb.Append(pi.ToString() + ",");
                    sb.Append(lid.ToString() + ")");
                    Database db = DatabaseFactory.CreateDatabase("dbconn");
                    System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                    db.ExecuteNonQuery(dbc);
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// 中心端--determine whether rule log existed
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        public bool? IsRuleLogExisted_c(long lid, int pi)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select count(*) from t_RulelogS t where ");
                sb.Append("t.plantid=" + pi.ToString() + " and ");
                sb.Append("t.id=" + lid.ToString());
                Database db = DatabaseFactory.CreateDatabase("dbconn");
                System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                if ((int)db.ExecuteScalar(dbc) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="ts"></param>
        /// <param name="pn"></param>
        /// <param name="rt"></param>
        /// <param name="rd"></param>
        /// <param name="cemstype"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        [WebMethod]
        public int UpdateRuleLog_c(long lid, DateTime ts, string pn, /*string rn,*/ string rt, string rd, string cemstype,/*int mi,*/ int pi)
        {
            try
            {
                if (IsRuleLogExisted_c(lid, pi) == false)
                {
                    //not existed
                    AddRuleLogRd_c(lid, ts, pn, rt, rd, cemstype, pi);
                    return 0;
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("update t_RulelogS set timelog='");
                    sb.Append(ts.ToString("yyyy-MM-dd HH:mm:ss") + "',rulename='");
                    sb.Append(pn.ToString() + "',alarmlog='");
                    sb.Append(rt.ToString() + "',alarmdis='");
                    sb.Append(rd.ToString() + "',cemstype='");
                    sb.Append(cemstype.ToString() + "' where plantid=");
                    sb.Append(pi.ToString() + " and id=");
                    sb.Append(lid.ToString());
                    Database db = DatabaseFactory.CreateDatabase("dbconn");
                    System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                    db.ExecuteNonQuery(dbc);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        #endregion

        #region Monitor Data

        /// <summary>
        /// 中心端--环保小时均值
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="ts"></param>
        /// <param name="enterprise"></param>
        /// <param name="pn"></param>
        /// <param name="indicatorid"></param>
        /// <param name="indicatorvalue"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        [WebMethod]
        public int AddMonitorData_c(long lid, DateTime ts, string enterprise, string pn, /*string rn,*/ int indicatorid, double indicatorvalue, /*int mi,*/ int pi)
        {
            try
            {
                if (IsMonitorDataExisted_c(lid, pi) == false)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("insert into envirmonitordata(timestamps,enterprise,pointname,indicatorid,indicatorvalue,plantid,id) values('");
                    sb.Append(ts.ToString("yyyy-MM-dd HH:mm:ss") + "','");
                    sb.Append(enterprise.ToString() + "','");
                    sb.Append(pn.ToString() + "',");
                    sb.Append(indicatorid.ToString() + ",");
                    sb.Append(indicatorvalue.ToString() + ",");
                    sb.Append(pi.ToString() + ",");
                    sb.Append(lid.ToString() + ")");
                    Database db = DatabaseFactory.CreateDatabase("dbconn");
                    System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                    db.ExecuteNonQuery(dbc);
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// 中心端--determine whether monitor data existed
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        public bool? IsMonitorDataExisted_c(long lid, int pi)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select count(*) from envirmonitordata t where ");
                sb.Append("t.plantid=" + pi.ToString() + " and ");
                sb.Append("t.id=" + lid.ToString());
                Database db = DatabaseFactory.CreateDatabase("dbconn");
                System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                if ((int)db.ExecuteScalar(dbc) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="ts"></param>
        /// <param name="enterprise"></param>
        /// <param name="pn"></param>
        /// <param name="indicatorid"></param>
        /// <param name="indicatorvalue"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        [WebMethod]
        public int UpdateMonitorData_c(long lid, DateTime ts, string enterprise, string pn, /*string rn,*/ int indicatorid, double indicatorvalue, /*int mi,*/ int pi)
        {
            try
            {
                if (IsMonitorDataExisted_c(lid, pi) == false)
                {
                    //not existed
                    AddMonitorData_c(lid, ts, enterprise, pn, indicatorid, indicatorvalue, pi);
                    return 0;
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("update envirmonitordata set timestamps='");
                    sb.Append(ts.ToString("yyyy-MM-dd HH:mm:ss") + "',enterprise='");
                    sb.Append(enterprise.ToString() + "',pointname='");
                    sb.Append(pn.ToString() + "',indicatorid=");
                    sb.Append(indicatorid.ToString() + ",indicatorvalue=");
                    sb.Append(indicatorvalue.ToString() + " where plantid=");
                    sb.Append(pi.ToString() + " and id=");
                    sb.Append(lid.ToString());
                    Database db = DatabaseFactory.CreateDatabase("dbconn");
                    System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                    db.ExecuteNonQuery(dbc);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        #endregion

        #region Exception Group
        /// <summary>
        /// 中心端--异常分组
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="envirid"></param>
        /// <param name="typeid"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        [WebMethod]
        public int AddExceptionGroup_c(long lid, long envirid, int typeid, /*int mi,*/ int pi)
        {
            try
            {
                if (IsExceptionGroupExisted_c(envirid, pi) == false)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("insert into exceptiondata_group(envir_id,typeid,plantid) values(");
                    sb.Append(envirid.ToString() + ",");
                    sb.Append(typeid.ToString() + ",");
                    sb.Append(pi.ToString() + ")");
                    //modified 2015/04/09
                    //sb.Append(lid.ToString() + ")");
                    Database db = DatabaseFactory.CreateDatabase("dbconn");
                    System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                    db.ExecuteNonQuery(dbc);
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// 中心端--determine whether exception group data existed
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        /// modified 2015-04-09
        public bool? IsExceptionGroupExisted_c(long envid, int pi)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select count(*) from exceptiondata_group t where ");
                sb.Append("t.plantid=" + pi.ToString() + " and ");
                sb.Append("t.envir_id=" + envid.ToString());
                Database db = DatabaseFactory.CreateDatabase("dbconn");
                System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                if ((int)db.ExecuteScalar(dbc) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="envirid"></param>
        /// <param name="typeid"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        [WebMethod]
        public int UpdateExceptionGroup_c(long lid, long envirid, int typeid, /*int mi,*/ int pi)
        {
            try
            {
                if (IsExceptionGroupExisted_c(envirid, pi) == false)
                {
                    //not existed
                    AddExceptionGroup_c(lid, envirid, typeid, pi);
                    return 0;
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("update exceptiondata_group set ");
                    sb.Append("typeid=");
                    sb.Append(typeid.ToString() + " where plantid=");
                    sb.Append(pi.ToString() + " and envir_id=");
                    sb.Append(envirid.ToString());
                    Database db = DatabaseFactory.CreateDatabase("dbconn");
                    System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                    db.ExecuteNonQuery(dbc);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        #endregion

        #region Exception_RuleLog_Match
        /// <summary>
        /// 中心端--异常关联规则日志
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="envirid"></param>
        /// <param name="ruleid"></param>
        /// <param name="typeid"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        [WebMethod]
        public int AddExceptionRuleLog_c(long lid, long envirid, long ruleid, int typeid, /*int mi,*/ int pi)
        {
            try
            {
                if (IsExceptionRuleLogExisted_c(lid, pi, envirid, ruleid, typeid) == false)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("insert into envirexception_rulelog_match(envir_id,rule_id,typeid,plantid) values(");
                    sb.Append(envirid.ToString() + ",");
                    sb.Append(ruleid.ToString() + ",");
                    sb.Append(typeid.ToString() + ",");
                    sb.Append(pi.ToString() + ")");
                    //sb.Append(lid.ToString() + ")");
                    Database db = DatabaseFactory.CreateDatabase("dbconn");
                    System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                    db.ExecuteNonQuery(dbc);
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// 中心端--determine whether exception rulelog data existed
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        public bool? IsExceptionRuleLogExisted_c(long lid, int pi, long envirid, long ruleid, int typeid)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select count(*) from envirexception_rulelog_match t where ");
                sb.Append("t.plantid=" + pi.ToString() + " and ");
                sb.Append("t.envir_id=" + envirid.ToString() + " and ");
                sb.Append("t.rule_id=" + ruleid.ToString() + " and ");
                sb.Append("t.typeid=" + typeid.ToString());
                Database db = DatabaseFactory.CreateDatabase("dbconn");
                System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                if ((int)db.ExecuteScalar(dbc) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="envirid"></param>
        /// <param name="ruleid"></param>
        /// <param name="typeid"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        //[WebMethod]
        public int UpdateExceptionRuleLog_c(long lid, long envirid, long ruleid, int typeid, /*int mi,*/ int pi)
        {
            try
            {
                if (IsExceptionRuleLogExisted_c(lid, pi, envirid, ruleid, typeid) == false)
                {
                    //not existed
                    AddExceptionRuleLog_c(lid, envirid, ruleid, typeid, pi);
                    return 0;
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("update envirexception_rulelog_match set ");
                    sb.Append("rule_id=");
                    sb.Append(ruleid.ToString() + ",typeid=");
                    sb.Append(typeid.ToString() + " where plantid=");
                    sb.Append(pi.ToString() + " and envir_id=");
                    sb.Append(envirid.ToString());
                    Database db = DatabaseFactory.CreateDatabase("dbconn");
                    System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                    db.ExecuteNonQuery(dbc);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        #endregion

        #region PI Avg Rds

        [WebMethod]
        public int AddPiAvgRecord_c(string pn, float pv, DateTime ts, DateTime ut, int pi)
        {
            try
            {
                if (IsPiAvgRdExisted_c(pn,ts, pi) == false)
                {
                    //not existed
                    StringBuilder sb = new StringBuilder();
                    sb.Append("insert into PiAvgRecords(pname,timestamps,pvalue,updatetime,plantid) values('");
                    sb.Append(pn.ToString() + "','");
                    sb.Append(ts.ToString("yyyy-MM-dd HH:mm:ss") + "',");
                    sb.Append(pv.ToString() + ",'");
                    sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
                    sb.Append(pi.ToString() + ")");
                    Database db = DatabaseFactory.CreateDatabase("dbconn");
                    System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                    db.ExecuteNonQuery(dbc);
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="ts"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        public bool? IsPiAvgRdExisted_c(string pn,DateTime ts, int pi)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select count(*) from PiAvgRecords t where ");
                sb.Append("t.plantid=" + pi.ToString() + " and ");
                sb.Append("t.timestamps='" + ts.ToString("yyyy-MM-dd HH:mm:ss") + "' and ");
                sb.Append("t.pname='" + pn.ToString() + "'");
                Database db = DatabaseFactory.CreateDatabase("dbconn");
                System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                if ((int)db.ExecuteScalar(dbc) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region Calib Rds
        [WebMethod]
        public int AddCalibRecord_c(string pn, DateTime st, DateTime et, int pi)
        {
            try
            {
                if (IsCalibRdExisted_c(pn, st, et, pi) == false)
                {
                    //not existed
                    StringBuilder sb = new StringBuilder();
                    sb.Append("insert into CalibRecords(pointid,starttime,endtime,updatetime,plantid) values('");
                    sb.Append(pn.ToString() + "','");
                    sb.Append(st.ToString("yyyy-MM-dd HH:mm:ss") + "','");
                    sb.Append(et.ToString("yyyy-MM-dd HH:mm:ss") + "','");
                    sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
                    sb.Append(pi.ToString() + ")");
                    Database db = DatabaseFactory.CreateDatabase("dbconn");
                    System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                    db.ExecuteNonQuery(dbc);
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="ts"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        public bool? IsCalibRdExisted_c(string pn, DateTime st, DateTime et, int pi)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select count(*) from CalibRecords t where ");
                sb.Append("t.plantid=" + pi.ToString() + " and ");
                sb.Append("t.starttime='" + st.ToString("yyyy-MM-dd HH:mm:ss") + "' and ");
                sb.Append("t.endtime='" + et.ToString("yyyy-MM-dd HH:mm:ss") + "' and ");
                sb.Append("t.pointid='" + pn.ToString() + "'");
                Database db = DatabaseFactory.CreateDatabase("dbconn");
                System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                if ((int)db.ExecuteScalar(dbc) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region Calib Rule Value
        [WebMethod]
        public int AddCalibRuleValueRecord_c(string pn, float pv, DateTime ts, DateTime ut, int pi, int am)
        {
            try
            {
                if (IsCalibRuleValueRdExisted_c(pn, ts, pi) == false)
                {
                    //not existed
                    StringBuilder sb = new StringBuilder();
                    sb.Append("insert into CalibRuleValue(pname,timestamps,pvalue,actualminutes,updatetime,plantid) values('");
                    sb.Append(pn.ToString() + "','");
                    sb.Append(ts.ToString("yyyy-MM-dd HH:mm:ss") + "',");
                    sb.Append(pv.ToString() + ",");
                    sb.Append(am.ToString() + ",'");
                    sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
                    sb.Append(pi.ToString() + ")");
                    Database db = DatabaseFactory.CreateDatabase("dbconn");
                    System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                    db.ExecuteNonQuery(dbc);
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="ts"></param>
        /// <param name="pi"></param>
        /// <returns></returns>
        public bool? IsCalibRuleValueRdExisted_c(string pn, DateTime ts, int pi)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select count(*) from CalibRuleValue t where ");
                sb.Append("t.plantid=" + pi.ToString() + " and ");
                sb.Append("t.timestamps='" + ts.ToString("yyyy-MM-dd HH:mm:ss") + "' and ");
                sb.Append("t.pname='" + pn.ToString() + "'");
                Database db = DatabaseFactory.CreateDatabase("dbconn");
                System.Data.Common.DbCommand dbc = db.GetSqlStringCommand(sb.ToString());
                if ((int)db.ExecuteScalar(dbc) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion
    }
}