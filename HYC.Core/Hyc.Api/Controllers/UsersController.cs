﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Hyc.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hyc.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        /// <summary>
        /// 操作数据库接口
        /// </summary>
        private IUserService _userTodo { get; set; }

        /// <summary>
        /// 连接数据库字符串
        /// </summary>
        private string _connectionstring { get; set; }

        public UsersController(IUserService userTodo, IConfigurationRoot configuration)
        {
            this._userTodo = userTodo;
            this._connectionstring = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            
            var u = this._userTodo.CheckUser("admin","123");
            return new ObjectResult(u);
        }
        //[HttpGet]
        //public async Task<User> Get()
        //{
        //    User u = await this._userTodo.RetriveOneEntityByIdAsync(10000, _connectionstring);
        //    return u;
        //}

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
