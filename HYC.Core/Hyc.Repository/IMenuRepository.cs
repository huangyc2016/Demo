using Hyc.Model.TableModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hyc.Repository
{
    public interface IMenuRepository : IBaseRepository<Menu>
    {
        IEnumerable<Menu> LoadPageList(int parentId, int startPage, int pageSize, out int rowCount, string connectionString = null);


        bool DeleteEntityById(int id, string connectionString = null);
    }
}
