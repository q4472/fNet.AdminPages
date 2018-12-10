using Nskd;
using System;
using System.Data;

namespace FNet.AdminPages.Models
{
    public class F0Model
    {
        public FilteredData Data;
        public class FilteredData {
            private DataTable table;
            public FilteredData (DataTable dataTable)
            {
                table = dataTable;
            }
            public class RowItems
            {
                public String зачёт_uid;
                public String обработано;
                public String разрешено;
                public String дата;
                public String менеджер;
                public String аукцион;
                public String спецификация;
                public String товар_uid;
                public String товар;
                public String количество;
            }
            public RowItems GetRowItems(Int32 rowIndex)
            {
                DataRow dr = table.Rows[rowIndex];
                RowItems items = new RowItems
                {
                    зачёт_uid = dr["зачёт_uid"].ToString(),
                    обработано = (dr["обработано"] == DBNull.Value) ? "False" : ((Boolean)dr["обработано"]).ToString(),
                    разрешено = (dr["разрешено"] == DBNull.Value) ? "False" : ((Boolean)dr["разрешено"]).ToString(),
                    дата = (dr["дата"] == DBNull.Value) ? "" : ((DateTime)dr["дата"]).ToString("dd.MM.yy"),
                    менеджер = (dr["менеджер"] == DBNull.Value) ? "" : (String)dr["менеджер"],
                    аукцион = (dr["аукцион"] == DBNull.Value) ? "" : (String)dr["аукцион"],
                    спецификация = (dr["спецификация"] == DBNull.Value) ? "" : ((Int32)dr["спецификация"]).ToString(),
                    товар_uid = dr["товар_uid"].ToString(),
                    товар = (dr["товар"] == DBNull.Value) ? "" : (String)dr["товар"],
                    количество = (dr["количество"] == DBNull.Value) ? "" : ((Decimal)dr["количество"]).ToString("n3")
                };
                return items;
            }
            public Int32 RowCount { get { return table.Rows.Count; } }
        }
        public FilterData Filter;
        public class FilterData
        {
            public String все;
            public String разрешено;
            public String не_разрешено;
            public String дата_min;
            public String дата_max;
            public String менеджер;
            public String аукцион;
            public String спецификация_min;
            public String спецификация_max;
            public FilterData() { }
        }
        public F0Model()
        {
            Filter = new FilterData()
            {
                все = "False",
                разрешено = "True",
                не_разрешено = "True",
                дата_min = "",
                дата_max = "",
                менеджер = "",
                аукцион = "",
                спецификация_min = "",
                спецификация_max = ""
            };
        }
        public void Get(Guid sessionId, RequestPackage rqp0)
        {
            if (rqp0 != null)
            {
                sessionId = rqp0.SessionId;
                Filter.все = rqp0["все"] as String;
                Filter.спецификация_min = (rqp0["спецификация_min"] == null) ? "": rqp0["спецификация_min"].ToString();
                Filter.спецификация_max = (rqp0["спецификация_max"] == null) ? "" : rqp0["спецификация_max"].ToString();
            }
            RequestPackage rqp = new RequestPackage()
            {
                SessionId = sessionId,
                Command = "[Pharm-Sib].[dbo].[спецификации_зачёт_get]",
                Parameters = new RequestParameter[] {
                    new RequestParameter() { Name = "все", Value = Filter.все },
                    new RequestParameter() { Name = "разрешено", Value = Filter.разрешено },
                    new RequestParameter() { Name = "не_разрешено", Value = Filter.не_разрешено }
                }
            };
            if (!String.IsNullOrWhiteSpace(Filter.дата_min)) rqp["дата_min"] = Filter.дата_min;
            if (!String.IsNullOrWhiteSpace(Filter.дата_max)) rqp["дата_max"] = Filter.дата_max;
            if (!String.IsNullOrWhiteSpace(Filter.менеджер)) rqp["менеджер"] = Filter.менеджер;
            if (!String.IsNullOrWhiteSpace(Filter.аукцион)) rqp["аукцион"] = Filter.аукцион;
            if (!String.IsNullOrWhiteSpace(Filter.спецификация_min)) rqp["спецификация_min"] = Filter.спецификация_min;
            if (!String.IsNullOrWhiteSpace(Filter.спецификация_max)) rqp["спецификация_max"] = Filter.спецификация_max;
            rqp.AddSessionIdToParameters();
            ResponsePackage rsp = rqp.GetResponse("http://127.0.0.1:11012");
            if (rsp != null && rsp.Data != null && rsp.Data.Tables != null && rsp.Data.Tables.Count > 0)
            {
                Data = new FilteredData(rsp.Data.Tables[0]);
            }
        }
        public void ApplyFilter(RequestPackage rqp)
        {
            rqp.Command = "[Pharm-Sib].[dbo].[спецификации_зачёт_get]";
            ResponsePackage rsp = rqp.GetResponse("http://127.0.0.1:11012");
            if (rsp != null && rsp.Data != null && rsp.Data.Tables != null && rsp.Data.Tables.Count > 0)
            {
                Data = new FilteredData(rsp.Data.Tables[0]);
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
    }
}