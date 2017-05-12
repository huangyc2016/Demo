using Hyc.Model.TableModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hyc.Repository
{
    public interface IMenuRepository : IBaseRepository<Menu>
    {
        IEnumerable<Menu> LoadPageList(Guid parentId, int startPage, int pageSize, out int rowCount, string connectionString = null);

        Menu RetriveOneEntityById(Guid id, string connectionString = null);

        bool DeleteEntityById(Guid id, string connectionString = null);
    }
}
