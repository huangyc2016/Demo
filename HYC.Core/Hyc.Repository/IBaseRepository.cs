using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hyc.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// 添加一个实体
        /// </summary>
        /// <param name="entity">要创建的实体</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        bool InsertEntity(T entity, string connectionString = null);

        /// <summary>
        /// 根据主键Id获取一个实体
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        T RetriveOneEntityById(int id, string connectionString = null);


        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        IEnumerable<T> RetriveAllEntity(string connectionString = null);

        /// <summary>
        /// 修改一个实体
        /// </summary>
        /// <param name="entity">要修改的实体</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        bool UpdateEntity(T entity, string connectionString = null);

        /// <summary>
        /// 根据主键Id删除一个实体
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        bool DeleteEntityById(int id, string connectionString = null);


        /// <summary>
        /// 获取实体(根据特定的语句)
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        IEnumerable<T> RetriveEntity(string connectionString = null);
    }
}
