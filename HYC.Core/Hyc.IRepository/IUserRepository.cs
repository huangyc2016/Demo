using System;
using System.Collections.Generic;
using System.Text;

namespace Hyc.IRepository
{
    public interface IUserRepository<T> : IBaseRepository<T> where T : class
    {
    }
}
