using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Assimalign.OGraph.Internal;

internal static class EntityPropertyEntryExtensions
{
    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _cache;

    static EntityPropertyEntryExtensions()
    {
        _cache = new ConcurrentDictionary<Type, PropertyInfo[]>();
    }

    extension(Type type)
    {
        internal ConcurrentBag<EntityChange> CaptureEntityProperties(
            Func<object?> state,
            string? path = null)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Public | BindingFlags.Instance;

            ConcurrentBag<EntityChange> changes = new();

            // Get readable and writable properties
            PropertyInfo[] properties = _cache.GetOrAdd(type, type =>
            {
                return type.GetProperties(flags)
                    .Where(p =>
                    {
                        if (p.CanRead && p.CanWrite)
                        {
                            var attribute = Attribute.GetCustomAttribute(p, typeof(IgnoreOnChangeTrackingAttribute));

                            if (attribute is not null)
                            {
                                return false; // Ignore this property
                            }

                            return true;
                        }

                        return false;
                    })
                    .ToArray();
            });

            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];

                Type propertyType = property.PropertyType;
                string propertyName = property.Name;
                string propertyPath = path is null ? propertyName : string.Join('.', path, propertyName);
                Func<object, object> propertyGetter = property.GetValue!;

                

                if (propertyType.IsValueType || propertyType.IsEnum || propertyType == typeof(string))
                {
                    changes.Add(new EntityChange(
                        propertyName,
                        propertyPath,
                        state,
                        propertyGetter,
                        propertyType));
                }

                else if (propertyType.IsAssignableTo(typeof(IEnumerable)) && propertyType != typeof(string))
                {

                }

                else if (propertyType.IsClass)
                {
                    changes.Add(new EntityChange(
                        propertyName,
                        propertyPath,
                        state,
                        propertyGetter,
                        propertyType,
                        propertyType.CaptureEntityProperties(
                            state: () =>
                            {
                                var parent = state.Invoke();

                                if (parent is not null)
                                {
                                    return propertyGetter.Invoke(parent);
                                }

                                return null;
                            },
                            path: propertyPath).ToImmutableList()));
                }
            }

            return changes;
        }
    }
}
