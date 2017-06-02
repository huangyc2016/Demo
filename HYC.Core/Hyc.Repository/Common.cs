using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Hyc.Repository
{
    public class Common
    {
        public static PageDataView<T> GetPageData<T>(PageCriteria criteria, object param = null,string connectionString=null)
        {
            using (var conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                var p = new DynamicParameters();
                string proName = "ProcGetPageData";
                p.Add("@TableName", criteria.TableName);
                p.Add("@PrimaryKey", criteria.PrimaryKey);
                p.Add("@Fields", criteria.Fields);
                p.Add("@Condition", criteria.Condition);
                p.Add("@CurrentPage", criteria.CurrentPage);
                p.Add("@PageSize", criteria.PageSize);
                p.Add("@Sort", criteria.Sort);
                p.Add("@RecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);


                var pageData = new PageDataView<T>();
                pageData.Items = conn.Query<T>(proName, p, commandType: CommandType.StoredProcedure).ToList();

                pageData.TotalNum = p.Get<int>("@RecordCount");
                pageData.TotalPageCount = Convert.ToInt32(Math.Ceiling(pageData.TotalNum * 1.0 / criteria.PageSize));
                pageData.CurrentPage = criteria.CurrentPage > pageData.TotalPageCount ? pageData.TotalPageCount : criteria.CurrentPage;
                return pageData;
            }
        }

        public class PageCriteria
        {
            private string _TableName;
            public string TableName
            {
                get { return _TableName; }
                set { _TableName = value; }
            }
            private string _Fileds = "*";
            public string Fields
            {
                get { return _Fileds; }
                set { _Fileds = value; }
            }
            private string _PrimaryKey = "ID";
            public string PrimaryKey
            {
                get { return _PrimaryKey; }
                set { _PrimaryKey = value; }
            }
            private int _PageSize = 10;
            public int PageSize
            {
                get { return _PageSize; }
                set { _PageSize = value; }
            }
            private int _CurrentPage = 1;
            public int CurrentPage
            {
                get { return _CurrentPage; }
                set { _CurrentPage = value; }
            }
            private string _Sort = string.Empty;
            public string Sort
            {
                get { return _Sort; }
                set { _Sort = value; }
            }
            private string _Condition = string.Empty;
            public string Condition
            {
                get { return _Condition; }
                set { _Condition = value; }
            }
            private int _RecordCount;
            public int RecordCount
            {
                get { return _RecordCount; }
                set { _RecordCount = value; }
            }
        }
    }
}
