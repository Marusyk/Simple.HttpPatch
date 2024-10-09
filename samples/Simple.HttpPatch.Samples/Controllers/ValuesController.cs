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
            _person = new Person(1, "Bob", 18, new Guid(), new DateTime(1992, 2, 14));
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            return _person.ToString();
        }

        [HttpPatch]
        public IEnumerable<string> Patch([ModelBinder(BinderType = typeof(PatchObjectModelBinder<Person>))] Patch<Person> person)
        {
            string original = _person.ToString();

            person.Apply(_person);

            return new[]
            {
                $"old: {original}",
                $"new: {_person}"
            };
        }
    }
}
