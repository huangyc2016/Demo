using Dapper;
using Hyc.Model.TableModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hyc.Repository
{
    public class ControllerRepository : IControllerRepository
    {
        /// <summary>
        /// 创建一个用户角色
        /// </summary>
        /// <param name="entity">用户角色</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public bool InsertEntity(Controllers entity, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string insertSql = @"INSERT INTO [dbo].[Controller]
                                            ([Name],[Description])
                                      VALUES
                                            (@Name,@Description)";
                return conn.Execute(insertSql, entity) > 0;
            }
        }

        /// <summary>
        /// 根据主键Id删除一个用户角色
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public bool DeleteEntityById(int id, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string deleteSql = @"DELETE FROM [dbo].[Controller]
                                             WHERE Id = @Id";
                return conn.Execute(deleteSql, new { Id = id }) > 0;
            }
        }

        /// <summary>
        /// 获取所有用户角色
        /// </summary>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public IEnumerable<Controllers> RetriveAllEntity(string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string querySql = @"SELECT [Id],[Name],[Description],(select count(*) as ActionNum FROM [dbo].[Action]  where ControllerId=c.Id) as ActionNum
                                       FROM [dbo].[Controller] as c";
                return conn.Query<Controllers>(querySql);
            }
        }


        /// <summary>
        /// 根据主键Id获取一个用户角色
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public Controllers RetriveOneEntityById(int id, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string querySql = @"SELECT [Id],[Name],[Description]
                                       FROM [dbo].[Controller]
                                      WHERE Id = @Id";
                return conn.QueryFirstOrDefault<Controllers>(querySql, new { Id = id });
            }
        }

        /// <summary>
        /// 修改一个用户角色
        /// </summary>
        /// <param name="entity">要修改的用户角色/param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public bool UpdateEntity(Controllers entity, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string updateSql = @"UPDATE [dbo].[Controller] SET [Name] = @Name,Description=@Description
                                      WHERE Id = @Id";
                return conn.Execute(updateSql, entity) > 0;
            }
        }
    }
}
