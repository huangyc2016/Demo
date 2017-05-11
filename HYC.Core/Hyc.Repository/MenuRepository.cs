using Dapper;
using Hyc.Model.TableModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hyc.Repository
{
    public class MenuRepository : IMenuRepository
    {/// <summary>
     /// 创建一个用户
     /// </summary>
     /// <param name="entity">用户</param>
     /// <param name="connectionString">链接字符串</param>
     /// <returns></returns>
        public bool CreateEntity(Menu entity, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string insertSql = @"INSERT INTO [dbo].[Menu]
                                            ([ParentId],[SerialNumber],[Name],[Code],[Url],[Type],[Icon],[CreateDate],[Remarks])
                                      VALUES
                                            (@ParentId,@SerialNumber,@Name,@Code,@Url,@Type,@Icon,@CreateDate,@Remarks)";
                return conn.Execute(insertSql, entity) > 0;
            }
        }

        /// <summary>
        /// 根据主键Id删除一个用户
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public bool DeleteEntityById(int id, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string deleteSql = @"DELETE FROM [dbo].[Menu]
                                     WHERE Id = @Id";
                return conn.Execute(deleteSql, new { Id = id }) > 0;
            }
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public IEnumerable<Menu> RetriveAllEntity(string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string querySql = @"SELECT [Id],[ParentId],[SerialNumber],[Name],[Code],[Url],[Type],[Icon],[CreateDate],[Remarks]
                                       FROM [dbo].[Menu]";
                return conn.Query<Menu>(querySql);
            }
        }

        public IEnumerable<Menu> RetriveEntity(string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string querySql = @"SELECT [Id],[ParentId],[SerialNumber],[Name],[Code],[Url],[Type],[Icon],[CreateDate],[Remarks]
                                       FROM [dbo].[Menu]";
                return conn.Query<Menu>(querySql);
            }
        }


        /// <summary>
        /// 根据主键Id获取一个用户
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public Menu RetriveOneEntityById(int id, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string querySql = @"[Id],[ParentId],[SerialNumber],[Name],[Code],[Url],[Type],[Icon],[CreateDate],[Remarks]
                                       FROM [dbo].[Menu]
                                      WHERE Id = @Id";
                return conn.QueryFirstOrDefault<Menu>(querySql, new { Id = id });
            }
        }

        /// <summary>
        /// 修改一个用户
        /// </summary>
        /// <param name="entity">要修改的用户</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public bool UpdateEntity(Menu entity, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string updateSql = @"UPDATE [dbo].[Menu]
                                        SET [SerialNumber] = @SerialNumber
                                           ,[Name] = @Name
                                           ,[Code] = @Code
                                           ,[Type] = @Type
                                           ,[Icon] = @Icon
                                           ,[Remarks] = @Remarks
                                      WHERE Id = @Id";
                return conn.Execute(updateSql, entity) > 0;
            }
        }
    }
}
