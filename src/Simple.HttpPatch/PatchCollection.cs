using System;
using System.Collections.Generic;

namespace Simple.HttpPatch
{
    public class PatchCollection<TModel> : List<Patch<TModel>> where TModel : class, new()
    {
        public void Apply(IList<TModel> deltas)
        {
            if (deltas is null)
            {
                throw new ArgumentNullException(nameof(deltas));
            }

            for (int i = 0; i < deltas.Count; i++)
            {
                this[i].Apply(deltas[i]);
            }
        }
    }
}
