using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public static class OGraphExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="graph"></param>
    /// <returns></returns>
    public static IEnumerable<Route> GetRoutes(this IOGraph graph)
    {
        var routes = new Queue<Route>();

        foreach (var node in graph.Nodes)
        {
            Get(routes, node);
        }

        return routes;

        void Get(Queue<Route> routes, IOGraphNode node, string segment = default)
        {
            // Push Route Route
            routes.Enqueue($"/{segment}/{node.Label}");

            foreach (var edge in node.Edges)
            {
                var current = $"/{segment}/{node.Label}/{{{node.Label}Id}}";

                routes.Enqueue(current);

                Get(routes, edge.Target, current);
            }
        }
    }


    public static void GetRouteTable(this IOGraph graph)
    {
        var list = new List<Route>();

        foreach (var node in graph.Nodes)
        {
            TraverseNode(node);
        }

        void TraverseNode(IOGraphNode node)
        {
            var root = node.Label.ToCamalCase();

            list.Add(root);
            list.Add($"{root}/{{id}}");

            foreach (var edge in node.Edges)
            {
                foreach (var path in TraverseEdge(edge))
                {
                    list.Add($"{root}/{{id}}/{path}");
                }
            }
        }

        IEnumerable<string> TraverseEdge(IOGraphEdge edge)
        {
            var paths = new List<string>();
            var root = edge.Target.Label.ToCamalCase();

            


            return paths;
        }
    }
}