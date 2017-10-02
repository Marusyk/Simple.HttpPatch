using System;

namespace TestWebApi.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public Guid Guid { get; set; }
        public DateTime? BirthDate { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}; Name: {Name}; Age: {Age}; Guid: {Guid}; BirthDate: {BirthDate};";
        }
    }
}
