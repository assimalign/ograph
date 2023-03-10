using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal
{
    internal class OGraphOperationDescriptor : IOGraphOperationDescriptor
    {
        private readonly OGraph graph;
        private readonly OGraphOperation operation;


        public OGraphOperationDescriptor(OGraphOperation operation, OGraph graph)
        {
            this.graph = graph;
            this.operation = operation;
        }

        public IOGraphOperationDescriptor UseMethod(Method method)
        {
            operation.Method = method;
            return this;
        }

        public IOGraphOperationDescriptor UseMiddleware(IOGraphOperationMiddleware middleware)
        {
            return this;
        }

        public IOGraphOperationDescriptor UseNodes(Label label)
        {
            operation.Node = graph.Nodes.FirstOrDefault(node => node.Label == label);
            return this;
        }

        public IOGraphOperationDescriptor UseQuery(QueryParam query)
        {
            return this;
        }

        public IOGraphOperationDescriptor UseResolver(IOGraphOperationResolver resolver)
        {
            operation.Resolver = resolver;
            return this;
        }

        public IOGraphOperationDescriptor UseResolver(OGraphOperationResolver resolver)
        {
            operation.Resolver = new OGraphOperationResolverDefault(resolver);
            return this;
        }

        public IOGraphOperationDescriptor UseRoute(Route route)
        {
            operation.Route = route;
            return this;
        }
    }
}
