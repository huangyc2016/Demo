using Dapper;
using Hyc.Model.TableModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

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
        public bool CreateEntity(User entity, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string insertSql = @"INSERT INTO [dbo].[User]
                                            ([UserName],[Password],[Name],[EMail],[MobileNumber],[LastLoginTime],[LoginTimes],[CreateDate],[IsDeleted])
                                      VALUES
                                            (@UserName,@Password,@Name,@EMail,@MobileNumber,@LastLoginTime,@LoginTimes,@CreateDate,@IsDeleted)";
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
                string querySql = @"SELECT [Id],[UserName],[Password],[Name],[EMail],[MobileNumber],[LastLoginTime],[LoginTimes],[CreateDate],[IsDeleted]
                                       FROM [dbo].[User]";
                return conn.Query<User>(querySql);
            }
        }

        public IEnumerable<User> RetriveEntity(string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string querySql = @"SELECT [Id],[UserName],[Password],[Name],[EMail],[MobileNumber],[LastLoginTime],[LoginTimes],[CreateDate],[IsDeleted]
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
                string querySql = @"SELECT [Id],[UserName],[Password],[Name],[EMail],[MobileNumber],[LastLoginTime],[LoginTimes],[CreateDate],[IsDeleted]
                                       FROM [dbo].[User]
                                      WHERE Id = @Id";
                return conn.QueryFirstOrDefault<User>(querySql, new { Id = id });
            }
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                return conn.QueryFirstOrDefault<User>("sp_GetUsers", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
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
                                      WHERE Id = @Id";
                return conn.Execute(updateSql, entity) > 0;
            }
        }

        public  User CheckUser(string UserName, string Password, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string querySql = @"SELECT [Id],[UserName],[Password],[Name],[EMail],[MobileNumber],[LastLoginTime],[LoginTimes],[CreateDate],[IsDeleted]
                                    FROM [dbo].[User]
                                    WHERE UserName = @UserName AND Password=@Password";
                return  conn.QueryFirstOrDefault<User>(querySql, new { UserName = UserName , Password = Password });
            }
        }
    }
}
