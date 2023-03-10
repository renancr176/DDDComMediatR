namespace ApiMvno.Infra.CrossCutting.Log;

public interface ILog
{
    Task LogAsync(LogLevelEnum logLevel, object data, string? traceIdentifier = null, string? index = null, CancellationToken cancellationToken = default);
    Task LogAsync(Exception exception, string? traceIdentifier = null, string? index = null, CancellationToken cancellationToken = default);
}