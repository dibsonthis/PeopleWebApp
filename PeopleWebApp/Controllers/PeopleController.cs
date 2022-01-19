using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleWebApp.Data;
using PeopleWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PeopleWebApp.Controllers
{
    public class PeopleController : Controller
    {
        public static List<Person> lastCreatedPeople = new List<Person>();

        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public PeopleController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task UpdateDB(List<Person> people)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _context.People.RemoveRange(await _context.People.Where(o => o.AppUserID == userId).ToListAsync());

            foreach (Person person in people)
            {
                await _context.AddAsync(person);
            }

            await _context.SaveChangesAsync();
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var people = await _context.People.Where(o => o.AppUserID == userId).ToListAsync();
            return View(people);
        }

        [Authorize]
        public IActionResult Generate()
        {
            return View(new List<Person>());
        }

        [HttpPost]
        public async Task<IActionResult> Generate(int numPeople)
        {
            var people = await PeopleGenerator.GetRandomPeople(numPeople);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int personID = 1;

            foreach (var person in people)
            {
                person.AppUserID = userId;
                person.PersonId = personID;
                personID++;
            }

            lastCreatedPeople = people;
            return View(people);
        }

        [HttpPost]
        public async Task<IActionResult> SavePeople()
        {
            await UpdateDB(lastCreatedPeople);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var person = await _context.People.Where(o => (o.PersonId == id) && o.AppUserID == userId).FirstOrDefaultAsync();

            if (person == null)
            {
                return View();
            }

            return View(person);
        }
    }
}
