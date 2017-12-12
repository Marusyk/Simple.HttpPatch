using System;

namespace Simple.HttpPatch.Samples.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        [PatchIgnore]
        public Guid Guid { get; set; }
        [PatchIgnoreNull]
        public DateTime BirthDate { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}; Name: {Name}; Age: {Age}; Guid: {Guid}; BirthDate: {BirthDate};";
        }
    }
}
