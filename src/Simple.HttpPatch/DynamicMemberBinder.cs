using System.Dynamic;

namespace Simple.HttpPatch;

public class DynamicMemberBinder : SetMemberBinder
{
    public DynamicMemberBinder(string name, bool ignoreCase) : base(name, ignoreCase)
    {
    }

    public override DynamicMetaObject FallbackSetMember(DynamicMetaObject target, DynamicMetaObject value,
        DynamicMetaObject errorSuggestion)
    {
        throw new System.NotImplementedException();
    }
}