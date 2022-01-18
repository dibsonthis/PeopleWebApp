using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PeopleWebApp
{
    public static class PeopleGenerator
    {
        public static async Task<List<Person>> GetRandomPeople(int numPeople = 1)
        {
            var request = await APIHelper.APIClient.GetAsync($"https://randomuser.me/api?results={numPeople}&nat=us,fr,gb");
            if (request.IsSuccessStatusCode)
            {
                var content = await request.Content.ReadAsAsync<PersonData>();
                var people = new List<Person>();

                foreach (var result in content.Results)
                {
                    Person person = new Person(result);

                    var imageRequest = await APIHelper.APIClient.GetAsync($"https://fakeface.rest/face/json?gender={person.Gender}&minimum_age=21");
                    var imageData = await imageRequest.Content.ReadAsAsync<Dictionary<string, string>>();
                    var image = imageData["image_url"];

                    person.Image = image;
                    people.Add(person);
                }

                return people;
            }
            else
            {
                throw new Exception(request.ReasonPhrase);
            }
        }
    }
}
