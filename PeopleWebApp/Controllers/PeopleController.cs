using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleWebApp.Data;
using PeopleWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PeopleWebApp.Controllers
{
    public class PeopleController : Controller
    {
        public static List<Person> lastCreatedPeople = new List<Person>();

        private readonly AppDbContext _context;
        public PeopleController(AppDbContext context)
        {
            _context = context;
        }

        public async Task UpdateDB(List<Person> people)
        {
            _context.People.RemoveRange(_context.People.ToList());
            await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT('People',RESEED,1);"); // reset ID when clearing the table

            foreach (Person person in people)
            {
                await _context.AddAsync(person);
            }

            await _context.SaveChangesAsync();
        }
        public async Task<IActionResult> Index()
        {
            var people = await _context.People.ToListAsync();
            return View(people);
        }

        public IActionResult Generate()
        {
            return View(new List<Person>());
        }

        [HttpPost]
        public async Task<IActionResult> Generate(int numPeople)
        {
            var people = await PeopleGenerator.GetRandomPeople(numPeople);
            lastCreatedPeople = people;
            return View(people);
        }

        [HttpPost]
        public async Task<IActionResult> SavePeople()
        {
            await UpdateDB(lastCreatedPeople);
            return View("Index", lastCreatedPeople);
        }
    }
}
