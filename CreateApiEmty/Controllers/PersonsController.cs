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
    public class PersonsController : ControllerBase
    {
         PersonContext dbContext;

        public PersonsController(PersonContext context)
        {
            dbContext = context;
            if (!dbContext.Persons.Any())
            {
                dbContext.Persons.Add(new Person { FirstName = "Ali", LastName = "Vali", Address = "Hujand", Email = "alie@vali.com", PhoneNamber = "927758499", Login = "ali", Password = "vali" });
                dbContext.Persons.Add(new Person { FirstName = "Tom", LastName = "Nikolay", Address = "Hujand", Email = "alie@vali.com", PhoneNamber = "927758499", Login = "ali", Password = "vali" });
                dbContext.SaveChanges();
            }
        }

        [HttpGet("person")]
        public ActionResult<IEnumerable<Person>> UsersList()
        {
            if (dbContext == null)
            {
                //dbContext = GetServerContext();
            }
            return dbContext.Persons.ToList();
        }

        // GET: api/<PersonController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get()
        {
            return await dbContext.Persons.ToListAsync();
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            Person person = await dbContext.Persons.FirstOrDefaultAsync(x => x.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return new ObjectResult(person);
        }

        // POST api/<PersonController>
        [HttpPost]
        public async Task<ActionResult<Person>> Post(Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            dbContext.Persons.Add(person);
            await dbContext.SaveChangesAsync();
            return Ok(person);
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Person>> Put(Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            if (!dbContext.Persons.Any(x=>x.Id == person.Id))
            {
                return NotFound();
            }
            dbContext.Update(person);
            await dbContext.SaveChangesAsync();
            return Ok(person);
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> Delete(int id)
        {
            Person person = dbContext.Persons.FirstOrDefault(x => x.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            dbContext.Persons.Remove(person);
            await dbContext.SaveChangesAsync();
            return Ok(person);
        }
    }
}
