using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest: notnull, IRequest<TResponse> 
    where TResponse: notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handling request={RequestName} - Response={ResponseName} with content: {@RequestData}", 
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = Stopwatch.StartNew();
        timer.Start();

        var response = await next();

        timer.Stop();

        var timeTaken = timer.Elapsed;
        if(timeTaken.Seconds>3) // Log a warning if the request takes more than 3 seconds
            logger.LogWarning("[PERFORMANCE] Handling request={RequestName} - Response={ResponseName} took {TimeTaken} seconds",
                typeof(TRequest).Name, typeof(TResponse).Name, timeTaken.TotalSeconds);

        logger.LogInformation("[END] Handling request={RequestName} - Response={ResponseName} with content: {@ResponseData}",
            typeof(TRequest).Name, typeof(TResponse).Name, response);
        return response;
    }
}
