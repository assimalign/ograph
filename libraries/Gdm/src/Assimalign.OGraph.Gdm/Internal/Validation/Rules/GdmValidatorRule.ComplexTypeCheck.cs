using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmComplexTypeCheckRule : GdmValidatorRule
{
    /*
        Rules:
            1. No Complex Type property should be a key 
     
     */
    public override void OnValidate(GdmValidatorContext context)
    {
        var complexTypes = context.Model.Elements.OfType<IOGraphGdmComplexType>();

        foreach (var complexType in complexTypes)
        {
            var runtimeType = complexType.RuntimeType;

            if (runtimeType is null)
            {
                context.AddFailure(error =>
                {
                    error.Message = "";
                    error.Source = "";
                });
            }
            else if (!runtimeType.IsClass)
            {
                context.AddFailure(error =>
                {
                    error.Message = "";
                    error.Source = "";
                });
            }
            else if (runtimeType.IsAbstract)
            {
                context.AddFailure(error =>
                {
                    error.Message = "";
                    error.Source = "";
                });
            }
            // Since a delegate are actually compiled into a class. Let's check that 
            else if (runtimeType.IsSubclassOf(typeof(Delegate)))
            {
                context.AddFailure(error =>
                {
                    error.Message = "Delegates are not allowed as complex types.";
                    error.Source = complexType.Label;
                });
            }
            // 3. Check if type has default constructor
            else if (runtimeType.GetConstructor(Type.EmptyTypes) is null)
            {
                context.AddFailure(error =>
                {
                    error.Message = $"The type {runtimeType.Name} does not have a default constructor. {runtimeType.Name}.ctor()";
                    error.Source = complexType.Label;
                });
            }
            else if (runtimeType.IsAssignableTo(typeof(string)))
            {
                context.AddFailure(error =>
                {
                    error.Message = "System.String is not allowed as a complex type.";
                    error.Source = complexType.Label;
                });
            }
        }
    }
}
