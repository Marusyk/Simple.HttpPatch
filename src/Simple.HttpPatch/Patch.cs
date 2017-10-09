using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Simple.HttpPatch
{
    public sealed class Patch<TModel> : DynamicObject where TModel : class
    {
        private readonly IDictionary<PropertyInfo, object> _changedProperties = new Dictionary<PropertyInfo, object>();

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            PropertyInfo propertyInfo = typeof(TModel).GetProperty(binder.Name);
            if (propertyInfo != null)
            {
                bool isIgnoredPropery = propertyInfo.GetCustomAttributes<PatchIgnoreAttribute>().Any();
                if (!isIgnoredPropery)
                {
                    _changedProperties.Add(propertyInfo, value);
                }
            }

            return base.TrySetMember(binder, value);
        }

        public void Apply(TModel delta)
        {
            if(delta == null)
            {
                throw new ArgumentNullException(nameof(delta));
            }

            foreach (var property in _changedProperties)
            {
                if (!IsExcludedProperty(property.Key.Name))
                {
                    object value = ChangeType(property.Value, property.Key.PropertyType);
                    property.Key.SetValue(delta, value);
                }
            }
        }

        private static object ChangeType(object value, Type type)
        {
            try
            {
                if (type == typeof(Guid))
                {
                    return Guid.Parse((string)value);
                }

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    if (value == null)
                    {
                        return null;
                    }

                    type = Nullable.GetUnderlyingType(type);
                }

                return Convert.ChangeType(value, type);
            }
            catch
            {
                return null;
            }
        }

        private static bool IsExcludedProperty(string propertyName)
        {
            IEnumerable<string> defaultExcludedProperies = new[] { "ID" };
            return defaultExcludedProperies.Contains(propertyName.ToUpper());
        }
    }
}
