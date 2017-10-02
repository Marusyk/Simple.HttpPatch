using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace WebAPI.HttpPatch
{
    public sealed class Patch<TModel> : DynamicObject where TModel : class
    {
        private readonly IDictionary<PropertyInfo, object> _changedProperties = new Dictionary<PropertyInfo, object>();

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var propertyInfo = typeof(TModel).GetProperty(binder.Name);
            if (propertyInfo != null)
            {
                _changedProperties.Add(propertyInfo, value);
            }

            return base.TrySetMember(binder, value);
        }

        public void Apply(TModel delta)
        {
            foreach (var property in _changedProperties)
            {
                object value = ChangeType(property.Value, property.Key.PropertyType);
                property.Key.SetValue(delta, value);
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
    }
}
