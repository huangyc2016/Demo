using Dapper;
using Hyc.Model.TableModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hyc.Repository
{
    public class ActionRepository : IActionRepository
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public bool InsertEntity(Actions entity, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string insertSql = @"INSERT INTO [dbo].[Action]
                                            ([ControllerId],[Name],[Description])
                                      VALUES
                                            (@ControllerId,@Name,@Description)";
                return conn.Execute(insertSql, entity) > 0;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public bool DeleteEntityById(int id, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string deleteSql = @"DELETE FROM [dbo].[Action]
                                             WHERE Id = @Id";
                return conn.Execute(deleteSql, new { Id = id }) > 0;
            }
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public IEnumerable<Actions> RetriveAllEntity(string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string querySql = @"SELECT [Id],[ControllerId],[Name],[Description]
                                       FROM [dbo].[Action]";
                return conn.Query<Actions>(querySql);
            }
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="ControllerId"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public IEnumerable<Actions> RetriveAllEntity(int ControllerId,string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string querySql = @"SELECT [Id],[ControllerId],[Name],[Description]
                                       FROM [dbo].[Action] WHERE ControllerId=@ControllerId";
                return conn.Query<Actions>(querySql, new { ControllerId = ControllerId });
            }
        }


        /// <summary>
        /// 根据一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public Actions RetriveOneEntityById(int id, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string querySql = @"SELECT [Id],[ControllerId],[Name],[Description]
                                       FROM [dbo].[Action]
                                      WHERE Id = @Id";
                return conn.QueryFirstOrDefault<Actions>(querySql, new { Id = id });
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public bool UpdateEntity(Actions entity, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string updateSql = @"UPDATE [dbo].[Action] SET [ControllerId]=@ControllerId,[Name] = @Name,Description=@Description
                                      WHERE Id = @Id";
                return conn.Execute(updateSql, entity) > 0;
            }
        }
    }
}
