using Hyc.Model.TableModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hyc.Repository
{
    
    public interface IUserRepository : IBaseRepository<User>
    {
        User CheckUser(string UserName, string Password, string connectionString = null);

        PageDataView<User> GetList(string userName, int page, int pageSize = 10, string connectionString = null);
    }
}
