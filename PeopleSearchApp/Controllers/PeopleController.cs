using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleSearchApp.DataAccessLayer.DBContexts;
using PeopleSearchApp.DataAccessLayer.Models;
using System.Threading;
using PeopleSearchApp.BusinessLayer;

namespace CodingAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleDBContext peopleContext;

        public PeopleController(PeopleDBContext peopleContext)
        {
            this.peopleContext = peopleContext;
        }

        // GET: api/People
        [HttpGet]
        public IEnumerable<Person> GetPeople([FromQuery] string searchString)
        {
            IEnumerable<Person> filteredPeople = new PersonNameSearchFilterer().ApplyCollectionFilter(peopleContext.People, searchString);

            //Simulating the retrieval being slow
            Thread.Sleep(5000);

            return filteredPeople;
        }

        // GET: api/People/n
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await peopleContext.People.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/People/n
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson([FromRoute] int id, [FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.Id)
            {
                return BadRequest();
            }

            //Since using an in-memory database, I needed 
            //to check if the current entity was being tracked
            //Then modify it
            var tempPerson = peopleContext.People.Find(id);
            peopleContext.Entry(tempPerson).CurrentValues.SetValues(person);
            peopleContext.Entry(tempPerson).State = EntityState.Modified;

            try
            {
                await peopleContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(person);
        }

        // POST: api/People
        [HttpPost]
        public async Task<IActionResult> PostPerson([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            peopleContext.People.Add(person);
            await peopleContext.SaveChangesAsync();

            return Ok(person);
        }

        // DELETE: api/People/n
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await peopleContext.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            peopleContext.People.Remove(person);
            await peopleContext.SaveChangesAsync();

            return Ok(string.Format("{0} has been deleted", person.FirstName));
        }

        private bool PersonExists(int id)
        {
            return peopleContext.People.Any(e => e.Id == id);
        }
    }
}