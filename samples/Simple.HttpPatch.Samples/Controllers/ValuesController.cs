using Microsoft.AspNetCore.Mvc;
using Simple.HttpPatch.Samples.Model;
using System;
using System.Collections.Generic;

namespace Simple.HttpPatch.Samples.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private Person _person;
        
        public ValuesController()
        {
            _person = new Person
            {
                Id = 1,
                Name = "Bob",
                Age = 18,
                Guid = new Guid(),
                BirthDate = null
            };
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            return _person.ToString();
        }

        [HttpPatch]
        public IEnumerable<string> Patch([FromBody] Patch<Person> person)
        {
            string original = _person.ToString();
            
            person.Apply(_person);

            return new[] 
            {
                $"old: {original}",
                $"new: {_person.ToString()}"
            };
        }
    }
}
