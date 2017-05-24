using Hyc.Model.TableModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hyc.Repository
{
    
    public interface IActionRepository : IBaseRepository<Actions>
    {

        IEnumerable<Actions> RetriveAllEntity(int ControllerId, string connectionString = null);
    }
}
