using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleWebApp.Data;
using PeopleWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleWebApp.Controllers
{
    [Route("api/People")]
    [ApiController]
    public class PeopleAPIController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PeopleAPIController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetPeople()
        {
            return await _context.People.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NoContent();
            }

            return person;
        }

        [HttpPost("new/random")]
        public async Task<ActionResult<Person>> CreatePerson()
        {
            var people = await PeopleGenerator.GetRandomPeople();
            var person = people[0];
            await _context.AddAsync(person);
            await _context.SaveChangesAsync();
            return person;
        }

        [HttpPost("new/random/{numPeople}")]
        public async Task<ActionResult<List<Person>>> CreatePerson(int numPeople)
        {
            var people = await PeopleGenerator.GetRandomPeople(numPeople);
            foreach(var person in people)
            {
                await _context.AddAsync(person);
            }
            await _context.SaveChangesAsync();
            return people;
        }

        [HttpPost("new")]
        public async Task<ActionResult<Person>> CreatePerson(Person person)
        {
            await _context.AddAsync(person);
            await _context.SaveChangesAsync();
            return person;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            var person = await _context.People.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePerson(int id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
