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
            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            operation.Middleware.Enqueue(middleware);

            return this;
        }

        public IOGraphOperationDescriptor UseMiddleware(OGraphOperationMiddleware middleware)
        {
            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            operation.Middleware.Enqueue(new OGraphOperationMiddlewareDefault(middleware));

            return this;
        }

        public IOGraphOperationDescriptor UseNodes(Label label)
        {
            operation.Node = graph.Nodes.FirstOrDefault(node => node.Label == label);
            return this;
        }

        public IOGraphOperationDescriptor UseQuery(QueryValue query)
        {
            return this;
        }

        public IOGraphOperationDescriptor UseResolver(IOGraphOperationResolver resolver)
        {
            if (resolver is null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }

            operation.Resolver = resolver;

            return this;
        }

        public IOGraphOperationDescriptor UseResolver(OGraphOperationResolver resolver)
        {
            operation.Resolver = new OGraphOperationResolverDefault(resolver);
            return this;
        }

        public IOGraphOperationDescriptor UseResolver<T>(OGraphOperationResolver<T> resolver)
        {
            throw new NotImplementedException();
        }

        public IOGraphOperationDescriptor UseRoute(Route route)
        {
            operation.Route = route;
            return this;
        }
    }
}
