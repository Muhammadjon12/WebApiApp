using CreateApiEmty.Model;
using CreateApiEmty.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CreateApiEmty.Controllers
{
    [Route("api/[controller]")]
   
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        UsersContext dbContext;
        public UsersController(UsersContext context)
        {
            dbContext = context;
            if (!dbContext.Users.Any())
            {
                dbContext.Users.Add(new User { Name = "Tom", Age = 26 });
                dbContext.Users.Add(new User { Name = "Alice", Age = 31 });
                dbContext.SaveChanges();
            }
        }
        [HttpGet("userss")]
        public ActionResult<IEnumerable<User>> UsersList()
        {
            if (dbContext == null)
            {
                //dbContext = GetServerContext();
            }
            return dbContext.Users.ToList();
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return dbContext.Users.ToList();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            User user = dbContext.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post(User user)
        {
            if (user.Age == 99)
                ModelState.AddModelError("Age", "Возраст не должен быть равен 99");

            if (user.Name == "admin")
            {
                ModelState.AddModelError("Name", "Недопустимое имя пользователя - admin");
            }
            // если есть лшибки - возвращаем ошибку 400
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dbContext.Users.Add(user);
             dbContext.SaveChangesAsync();
            return Ok(user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!dbContext.Users.Any(x => x.Id == user.Id))
            {
                return NotFound();
            }

            dbContext.Update(user);
            await dbContext.SaveChangesAsync();
            return Ok(user);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            User user = dbContext.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            dbContext.Users.Remove(user);
             dbContext.SaveChangesAsync();
            return Ok(user);
        }
    }
}
