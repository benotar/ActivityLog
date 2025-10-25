using System.Data.Common;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace ActivityLog.Chassis.EF;

public class QueryPerformanceInterceptor: DbCommandInterceptor
{
    private readonly ILogger<QueryPerformanceInterceptor> _logger;

    public QueryPerformanceInterceptor(ILogger<QueryPerformanceInterceptor> logger)
    {
        _logger = logger;
    }

    private const long QueryThreshold = 100;

    public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
    {
        var stopwatch = Stopwatch.StartNew();

        var interceptorResult = base.ReaderExecuting(command, eventData, result);
        
        stopwatch.Stop();

        if (stopwatch.ElapsedMilliseconds <= QueryThreshold)
        {
            return interceptorResult;
        }

        var commandText = command.CommandText;

        if (command.Parameters.Count > 0)
        {
            commandText += "| Parameters " + string.Join(", ", command.Parameters);
        }

        _logger.LogWarning(
            "Slow query detected: {CommandText} | Elapsed time: {ElapsedMillisends} ms",
            commandText,
            stopwatch.ElapsedMilliseconds);

        return interceptorResult;
    }
}
