
using System;
using System.Linq;
using System.Reflection;

namespace System;

internal static class TypeHelper
{
    public static Type Resolve(this Assembly assembly, string name, string ns = "")
    {
        Type? type;
        string fullName;

        // If namespace was provided
        if (!string.IsNullOrEmpty(ns))
        {
            fullName = string.Join(".", ns, name);
            type = assembly.GetType(fullName);

            if (type is null)
            {
                throw new InvalidOperationException($"Type '{fullName}' not found in assembly '{assembly.FullName}'.");
            }
        }

        // Try get name from assembly name

        fullName = string.Join(".", assembly.GetName().FullName, name);

        
        type = Type.GetType(fullName);

        // Last option, try to find type by name only
        if (type is null)
        {
            var types = assembly.GetTypes().Where(p => p.IsClass && p.Name == name);
            var count = types.Count();

            if (count > 1)
            {
                throw new InvalidOperationException($"Multiple types with name '{name}' found in assembly '{assembly.FullName}'.");
            }
            if (count == 0)
            {
                throw new InvalidOperationException($"Type '{name}' not found in assembly '{assembly.FullName}'.");
            }

            type = types.First();
        }

        return type;
    }

}
