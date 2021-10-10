using System;

namespace Simple.HttpPatch.Samples.Model
{
    public record Person(int Id, string Name, int? Age, [property: PatchIgnore] Guid Guid, [property: PatchIgnoreNull] DateTime BirthDate)
    {
        public override string ToString() => $"Id: {Id}; Name: {Name}; Age: {Age}; Guid: {Guid}; BirthDate: {BirthDate};";
    }
}
