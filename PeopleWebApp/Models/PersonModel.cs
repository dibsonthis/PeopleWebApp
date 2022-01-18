using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleWebApp.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public DateTime DOB { get; set; }
        public string Image { get; set; }

        public Person() { }
        public Person(Result result)
        {
            Gender = result.Gender;
            FirstName = result.Name.First;
            LastName = result.Name.Last;
            StreetNumber = result.Location.Street.Number;
            StreetName = result.Location.Street.Name;
            City = result.Location.City;
            State = result.Location.State;
            Country = result.Location.Country;
            Postcode = result.Location.Postcode;
            Email = result.Email;
            Phone = result.Phone;
            Cell = result.Cell;
            DOB = result.DOB.Date;
            Image = result.Picture.Large;
        }
    }

    public class PersonData
    {
        public Result[] Results { get; set; }
    }

    public class Result
    {
        [Key]
        public int PersonId { get; set; }
        public string Gender { get; set; }
        public Name Name { get; set; }
        public Location Location { get; set; }
        public string Email { get; set; }
        public Login Login { get; set; }
        public Dob DOB { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public Id Id { get; set; }
        public Picture Picture { get; set; }
        public string Nat { get; set; }
    }

    public class Name
    {
        public string Title { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
    }

    public class Location
    {
        public Street Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public Timezone Timezone { get; set; }
    }

    public class Street
    {
        public int Number { get; set; }
        public string Name { get; set; }
    }

    public class Timezone
    {
        public string Offset { get; set; }
        public string Description { get; set; }
    }

    public class Login
    {
        public string Uuid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Dob
    {
        public int DobID { get; set; }
        public DateTime Date { get; set; }
        public int Age { get; set; }
    }

    public class Id
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class Picture
    {
        public string Large { get; set; }
        public string Medium { get; set; }
        public string Thumbnail { get; set; }
    }
}
