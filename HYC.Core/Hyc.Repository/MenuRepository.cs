using Dapper;
using Hyc.Model.TableModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hyc.Repository
{
    public class MenuRepository : IMenuRepository
    {
        /// <summary>
        /// 创建一个用户
        /// </summary>
        /// <param name="entity">用户</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public bool InsertEntity(Menu entity, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string insertSql = @"INSERT INTO [dbo].[Menu]
                                            ([ParentId],[SortNum],[DepthNum],[Name],[Code],[Type],[Url],[Icon],[Description])
                                      VALUES
                                            (@ParentId,@SortNum,@DepthNum,@Name,@Code,@Type,@Url,@Icon,@Description)";
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
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        /// <returns></returns>
        public IEnumerable<Menu> RetriveAllEntity(string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string querySql = @"SELECT [Id],[ParentId],[SortNum],[DepthNum],[Name],[Code],[Type],[Url],[Icon],[Description]
                                       FROM [dbo].[Menu] ORDER BY SortNum ASC";
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
                string querySql = @"SELECT [Id],[ParentId],[SortNum],[DepthNum],[Name],[Code],[Type],[Url],[Icon],[Description]
                                       FROM [dbo].[Menu]
                                      WHERE Id = @Id ";
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
                string updateSql = @"UPDATE [dbo].[Menu] SET 
                                            [SortNum] = @SortNum
                                           ,[DepthNum] = @DepthNum
                                           ,[Name] = @Name
                                           ,[Url] = @Url
                                           ,[Code] = @Code
                                           ,[Type] = @Type
                                           ,[Icon] = @Icon
                                           ,[Description] = @Description
                                      WHERE Id = @Id";
                return conn.Execute(updateSql, entity) > 0;
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="startPage">页码</param>
        /// <param name="pageSize">单页数据数</param>
        /// <param name="rowCount">行数</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        public IEnumerable<Menu> LoadPageList(int parentId, int startPage, int pageSize, out int rowCount, string connectionString = null)
        {
            rowCount = 1;
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                return conn.Query<Menu>("sp_GetMenus", new { ParentId = parentId }, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Menu> GetListByDepthNum(int depthNum, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string querySql = @"SELECT [Id],[ParentId],[SortNum],[DepthNum],[Name],[Code],[Type],[Url],[Icon],[Description]
                                       FROM [dbo].[Menu] WHERE DepthNum=@DepthNum ORDER BY SortNum ASC";
                return conn.Query<Menu>(querySql, new { DepthNum = depthNum });
            }
        }

        public IEnumerable<Menu> GetListByParentId(int parentId, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string querySql = @"SELECT [Id],[ParentId],[SortNum],[DepthNum],[Name],[Code],[Type],[Url],[Icon],[Description]
                                       FROM [dbo].[Menu] WHERE ParentId=@ParentId ORDER BY SortNum ASC";
                return conn.Query<Menu>(querySql, new { ParentId = parentId });
            }
        }
    }
}
