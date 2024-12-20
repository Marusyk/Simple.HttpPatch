using Microsoft.AspNetCore.Mvc;
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
            PropertyInfo propertyInfo = typeof(TModel).GetProperty(binder.Name,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo is not null)
            {
                var isIgnoredProperty = propertyInfo.GetCustomAttribute<PatchIgnoreAttribute>() is not null;
                if (!isIgnoredProperty)
                {
                    var isIgnoredIfNull = propertyInfo.GetCustomAttribute<PatchIgnoreNullAttribute>() is not null;
                    if (isIgnoredIfNull)
                    {
                        if (value is not null)
                        {
                            _changedProperties.Add(propertyInfo, value);
                        }
                    }
                    else
                    {
                        _changedProperties.Add(propertyInfo, value);
                    }
                }
            }

            return base.TrySetMember(binder, value);
        }

        public void Apply(TModel delta)
        {
            if (delta is null)
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
                    if (value is null)
                    {
                        return null;
                    }

                    type = Nullable.GetUnderlyingType(type);
                }

                return Convert.ChangeType(value, type ?? throw new ArgumentNullException(nameof(type)));
            }
            catch
            {
                return null;
            }
        }

        private static bool IsExcludedProperty(string propertyName)
        {
            IEnumerable<string> defaultExcludedProperties = new[] { "ID" };
            return defaultExcludedProperties.Contains(propertyName.ToUpper());
        }
    }
}