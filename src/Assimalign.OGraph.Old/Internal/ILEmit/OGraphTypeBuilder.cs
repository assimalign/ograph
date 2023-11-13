using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal sealed class OGraphTypeBuilder
{
    private static ModuleBuilder assemblyBuilder = null;
    private static AssemblyName assembly = new AssemblyName() { Name = "AssimalignDynamicLinqTypes" };
    private static Dictionary<string, Type> builtTypes = new Dictionary<string, Type>();

    static OGraphTypeBuilder()
    {
        assemblyBuilder = AssemblyBuilder
            .DefineDynamicAssembly(assembly, AssemblyBuilderAccess.Run)
            .DefineDynamicModule(assembly.Name);
    }


    public static OGraphTypeBuilder Create(string name = "Anomymous", TypeAttributes attributes = TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Serializable)
    {
        return new OGraphTypeBuilder(assemblyBuilder.DefineType(name, attributes, typeof(object)));
    }




    private readonly TypeBuilder typeBuilder;

    public OGraphTypeBuilder(TypeBuilder typeBuilder)
    {
        this.typeBuilder = typeBuilder;
    }

    public FieldInfo EmitPrivateField(string fieldName, Type fieldType)
    {
        return typeBuilder.DefineField($"_{fieldName}", fieldType, FieldAttributes.Private);
    }
    public PropertyInfo EmitPublicProperty(string propertyName, Type propertyType)
    {
        var field = EmitPrivateField(propertyName, propertyType);
        var property = typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);

        var getMethodBuilder = typeBuilder.DefineMethod($"get_{propertyName}",
            MethodAttributes.Public |
            MethodAttributes.HideBySig |
            MethodAttributes.SpecialName,
            propertyType,
            Type.EmptyTypes);

        var setMethodBuilder = typeBuilder.DefineMethod($"set_{propertyName}",
            MethodAttributes.Public |
            MethodAttributes.HideBySig |
            MethodAttributes.SpecialName,
            null,
            new Type[] { propertyType });

        var getBuilder = getMethodBuilder.GetILGenerator();
        getBuilder.Emit(OpCodes.Ldarg_0);
        getBuilder.Emit(OpCodes.Ldfld, field);
        getBuilder.Emit(OpCodes.Ret);


        var setBuilder = setMethodBuilder.GetILGenerator();
        setBuilder.Emit(OpCodes.Ldarg_0);
        setBuilder.Emit(OpCodes.Ldarg_1);
        setBuilder.Emit(OpCodes.Stfld, field);
        setBuilder.Emit(OpCodes.Ret);

        property.SetGetMethod(getMethodBuilder);
        property.SetSetMethod(setMethodBuilder);


        return property;
    }

}
