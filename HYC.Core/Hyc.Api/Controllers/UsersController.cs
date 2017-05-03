using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hyc.Model.TableModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hyc.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private Repository.IUserRepository _UserTodo { get; set; }
        public UsersController(Repository.IUserRepository userTodo)
        {
            this._UserTodo = userTodo;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            User u = this._UserTodo.RetriveOneEntityById(10000, null);
            return new ObjectResult(u);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
