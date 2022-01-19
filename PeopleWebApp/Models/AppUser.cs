using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleWebApp.Models
{
    public class AppUser : IdentityUser
    {
        public List<Person> People { get; set; }
    }
}
