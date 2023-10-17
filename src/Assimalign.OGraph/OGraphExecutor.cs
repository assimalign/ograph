
using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;
using Assimalign.OGraph.Syntax;
using Assimalign.OGraph.Syntax.Internal;

public class OGraphExecutor : IOGraphExecutor
{
    private readonly OGraphOptions options;
    private readonly QueryParser parser;
    private readonly IOGraph graph;
    private readonly IServiceProvider? serviceProvider;

    public OGraphExecutor(IOGraph graph, OGraphOptions? options = null)
    {
        if (graph is null)
        {
            throw new ArgumentNullException(nameof(graph));
        }

        options ??= new OGraphOptions();

        this.graph = graph;
        this.options = options;
        this.parser = new QueryParser(options.ParserOptions);
        this.serviceProvider = options.ServiceProvider;
    }


    public virtual async Task<IOGraphExecutorResponse> ExecuteAsync(IOGraphExecutorRequest request, CancellationToken cancellationToken = default)
    {
        var response = new OGraphExecutorResponse();

        try
        {
            if (!TryValidate(request, out var error))
            {
                return response;
            }
            if (!TryGetOperation(request, out var operation))
            {
                // TODO: Return Not Found Response
                return response;
            }
            var hasQuery = default(bool);
            if (hasQuery = TryGetQuery(request, out var query))
            {
                if (!query.IsValid)
                {
                    // TODO : Bad Query
                    return response;
                }
            }
            var result = await operation!.ExecuteAsync(new OGraphResolverContext()
            {
                //Query           = query,
                //Operation       = operation,
                //ServiceProvider = serviceProvider,
                //Graph           = graph
            }, CancellationToken.None);

            switch (operation.OperationType)
            {
                case OperationType.Query when result is not IOGraphQueryResult && result is not IOGraphError:
                    {
                        // TODO: A query operation must return a IOGraphQueryResult or IOGraphErrorResult
                        break;
                    }
                case OperationType.Command:
                    {
                        break;
                    }
            }

            var responseBody = response.Body;

            await JsonSerializer.SerializeAsync(
                responseBody, 
                result, 
                options.JsonSerializerOptions);

            return response;
        }
        catch (Exception exception)
        {
            var internalErrorResponse = new OGraphExecutorResponse()
            {
                StatusCode = StatusCode.InternalServerError
            };

            return internalErrorResponse;
        }
    }

    private bool TryValidate(IOGraphExecutorRequest request, out IOGraphError error)
    {
        error = default;

        // 1. Validate Client Accept Header
        // 2. Validate Server Supported Media Types



        return true;

    }
    private bool TryGetOperation(IOGraphExecutorRequest request, out IOGraphOperation? operation)
    {
        operation = graph.Operations
            .FirstOrDefault(operation =>
            {
                return  operation.Method.Equals(request.Method) && 
                        operation.Route.IsMatch(request.Path, options.RoutePrefix!);
            });
        return operation is not null;
    }
    private bool TryGetQuery(IOGraphExecutorRequest request, out QueryDocument queryDocument)
    {
        queryDocument = default!;
        if (request.Query.TryGetValue("query", out var value))
        {
            queryDocument = parser.Parse(value);
            return true;
        }
        return false;
    }
    public static IOGraphExecutor Create(Name name, Action<OGraphOptions, IOGraphBuilder> configure)
    {
        var options = new OGraphOptions();
        var graph = OGraphBuilder.Create(name, builder =>
        {
            configure.Invoke(options, builder);
        });

        options.ParserOptions.AddAnalyzer(new InvalidChainingAnalyzer(graph));

        return new OGraphExecutor(graph, options);
    }
}
