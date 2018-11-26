using Nskd;
using System;
using System.Data;

namespace FNet.AdminPages.Models
{
    public class F0Model
    {
        public DataSet ChangeJournal;
        public class ItemArray
        {
            public String зачёт_uid;
            public String Х;
            public String ОК;
            public String Дата;
            public String Менеджер;
            public String Аукцион;
            public String Спецификация;
            public String товар_uid;
            public String Товар;
            public String Количество;
        }
        public F0Model() { }
        public void Get(Guid sessionId)
        {
            RequestPackage rqp = new RequestPackage()
            {
                SessionId = sessionId,
                Command = "[Pharm-Sib].[dbo].[спецификации_зачёт_get]"
            };
            rqp.AddSessionIdToParameters();
            ResponsePackage rsp = rqp.GetResponse("http://127.0.0.1:11012");
            if (rsp != null)
            {
                ChangeJournal = rsp.Data;
            }
        }
        public void ApplyFilter(RequestPackage rqp)
        {
            rqp.Command = "[Pharm-Sib].[dbo].[спецификации_зачёт_get]";
            ResponsePackage rsp = rqp.GetResponse("http://127.0.0.1:11012");
            if (rsp != null)
            {
                ChangeJournal = rsp.Data;
            }
        }
        public void Save(RequestPackage rqp)
        {
            if (rqp != null)
            {
                foreach (RequestParameter p in rqp.Parameters)
                {
                    String name = p.Name;
                    Boolean value = Convert.ToBoolean(p.Value);
                    Guid uid = Guid.Parse(name.Substring(0, 36));
                    String field = name.Substring(36);
                    RequestPackage rqp1 = new RequestPackage
                    {
                        SessionId = rqp.SessionId,
                        Command = "[Pharm-Sib].[dbo].[спецификации_зачёт_save]",
                        Parameters = new RequestParameter[] {
                            new RequestParameter { Name = "session_id", Value = rqp.SessionId },
                            new RequestParameter { Name = "товар_uid", Value = uid },
                            new RequestParameter { Name = field, Value = value }
                        }
                    };
                    ResponsePackage rsp = rqp1.GetResponse("http://127.0.0.1:11012");
                }
            }
        }
        public ItemArray GetRowItemArray(DataRow dr)
        {
            ItemArray items = new ItemArray
            {
                зачёт_uid = dr["зачёт_uid"].ToString(),
                Х = (dr["обработано"] == DBNull.Value) ? "False" : ((Boolean)dr["обработано"]).ToString(),
                ОК = (dr["разрешено"] == DBNull.Value) ? "False" : ((Boolean)dr["разрешено"]).ToString(),
                Дата = (dr["дата"] == DBNull.Value) ? "" : ((DateTime)dr["дата"]).ToString("dd.MM.yy"),
                Менеджер = (dr["менеджер"] == DBNull.Value) ? "" : (String)dr["менеджер"],
                Аукцион = (dr["аукцион"] == DBNull.Value) ? "" : (String)dr["аукцион"],
                Спецификация = (dr["спецификация"] == DBNull.Value) ? "" : ((Int32)dr["спецификация"]).ToString(),
                товар_uid = dr["товар_uid"].ToString(),
                Товар = (dr["товар"] == DBNull.Value) ? "" : (String)dr["товар"],
                Количество = (dr["количество"] == DBNull.Value) ? "" : ((Decimal)dr["количество"]).ToString("n3")
            };
            return items;
        }
    }
}