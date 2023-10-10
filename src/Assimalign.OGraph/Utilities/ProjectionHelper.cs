using System;
using Assimalign.OGraph.Syntax;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Utilities;

public static class ProjectionHelper
{

    public static async Task InvokeAsync(IOGraphNode node, ProjectionNode queryNode)
    {
        var type = node.Type as IOGraphComplexType;
        var context = default(IOGraphPropertyContext);

        queryNode.Properties.Select(property =>
        {
            if (type.Properties.TryGet(property.Name, out var graphProperty))
            {
                var resolver = graphProperty.BuildHandlerChain();

                var result = resolver.Invoke(context).Result;


            }
            else
            {
                throw new Exception();
            }
        });

        var tasks = type.Properties.Select(property =>
        {
            return Task.Run<IOGraphPropertyResult>(async () =>
            {
                try
                {
                    var result = await property.BuildHandlerChain().Invoke(context);

                    return result;
                }
                catch (Exception exception)
                {
                    return default(IOGraphPropertyResult);
                }
            });
        }).ToList();


        while (tasks.Any())
        {
            var complete = await Task.WhenAny(tasks);
            var result = complete.Result;



        }
    }



    private static IOGraphComplexType Traverse(IOGraphComplexType type, ProjectionNode projection)
    {
        var complexType = new ComplexType()
        {
            RuntimeType = type.RuntimeType,
            Name = type.Name,
        };

        foreach (var property in projection.Properties)
        {
            if (type.Properties.TryGet(property.Name, out var graphProperty))
            {
                if (graphProperty.Type is IOGraphComplexType nested)
                {
                    if (property.HasChildren)
                    {
                        throw new Exception();
                    }

                    property.Children
                }
                complexType.Properties.Add(graphProperty);
            }
            else
            {
                throw new Exception("Invalid Projection");
            }
        }

        return complexType;
    }

    //public static Task<TResults> ApplyProjectionsAsync<T, TResults>(IEnumerable<T> projections)
}
