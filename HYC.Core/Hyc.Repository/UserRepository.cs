using Dapper;
using Hyc.Model.TableModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using static Hyc.Repository.Common;

namespace Hyc.Repository
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// 创建一个用户
        /// </summary>
        /// <param name="entity">用户</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public bool InsertEntity(User entity, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string insertSql = @"INSERT INTO [dbo].[User]
                                            ([UserName],[Password],[Name],[EMail],[MobileNumber],[LastLoginTime],[RoleIds])
                                      VALUES
                                            (@UserName,@Password,@Name,@EMail,@MobileNumber,@RoleIds)";
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
                string deleteSql = @"DELETE FROM [dbo].[User]
                                             WHERE Id = @Id";
                return conn.Execute(deleteSql, new { Id = id }) > 0;
            }
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public IEnumerable<User> RetriveAllEntity(string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string querySql = @"SELECT [Id],[UserName],[Password],[Name],[EMail],[MobileNumber],[LastLoginTime],[LoginTimes],[CreateDate],[IsDeleted],[RoleIds]
                                       FROM [dbo].[User]";
                return conn.Query<User>(querySql);
            }
        }


        /// <summary>
        /// 根据主键Id获取一个用户
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public User RetriveOneEntityById(int id, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string querySql = @"SELECT [Id],[UserName],[Password],[Name],[EMail],[MobileNumber],[LastLoginTime],[LoginTimes],[CreateDate],[IsDeleted],[RoleIds]
                                       FROM [dbo].[User]
                                      WHERE Id = @Id";
                return conn.QueryFirstOrDefault<User>(querySql, new { Id = id });
            }
            //using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            //{
            //    return conn.QueryFirstOrDefault<User>("sp_GetUsers", new { Id = id }, commandType: CommandType.StoredProcedure);
            //}
        }
       
        /// <summary>
        /// 修改一个用户
        /// </summary>
        /// <param name="entity">要修改的用户</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public bool UpdateEntity(User entity, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string updateSql = @"UPDATE [dbo].[User]
                                        SET [UserName] = @UserName
                                           ,[Password] = @Password
                                           ,[Name] = @Name
                                           ,[EMail] = @EMail
                                           ,[MobileNumber] = @MobileNumber
                                           ,[IsDeleted] = @IsDeleted
                                           ,[RoleIds]=@RoleIds
                                      WHERE Id = @Id";
                return conn.Execute(updateSql, entity) > 0;
            }
        }

        public  User CheckUser(string UserName, string Password, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string querySql = @"SELECT [Id],[UserName],[Password],[Name],[EMail],[MobileNumber],[LastLoginTime],[LoginTimes],[CreateDate],[IsDeleted],[RoleIds]
                                    FROM [dbo].[User]
                                    WHERE UserName = @UserName AND Password=@Password";
                return  conn.QueryFirstOrDefault<User>(querySql, new { UserName = UserName , Password = Password });
            }
        }

        public  PageDataView<User> GetList(string userName, int page, int pageSize = 10, string connectionString = null)
        {
            PageCriteria criteria = new PageCriteria();
            criteria.Condition = "1=1";
            if (!string.IsNullOrEmpty(userName))
                criteria.Condition += string.Format(" and UserName = '{0}'", userName);
            criteria.CurrentPage = page;
            criteria.Fields = "*";
            criteria.PageSize = pageSize;
            criteria.TableName = "[User] as a";
            criteria.PrimaryKey = "Id";
            var r = Common.GetPageData<User>(criteria, null, connectionString);
            return r;
        }
    }
}
