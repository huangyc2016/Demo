using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hyc.Admin.Controllers
{
    //[Authorize(Roles = "Development")]
    //[Authorize(Policy = "UserOnly1")]
    [Authorize(Policy = "Over21")]
    [Authorize(Policy= "Passport")]
    public class UsersController : BaseController
    {
        private Repository.IUserRepository _UserTodo { get; set; }
        public UsersController(Repository.IUserRepository userTodo)
        {
            this._UserTodo = userTodo;
        }
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Movie.ToListAsync());
        //}
        ///[AllowAnonymous]
        public IActionResult Index()
        {
            var item = this._UserTodo.RetriveOneEntityById(4, null);
            return View(item);
        }
    }
}
